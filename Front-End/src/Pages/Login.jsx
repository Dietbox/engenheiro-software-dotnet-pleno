import { useEffect, useState } from "react";
import { IsEmail } from "../Utils";
import { Post } from "../Request";

export default function LoginPage({  }) {

    const [createAccountSuccess, setCreateAccountSuccess] = useState(!!(sessionStorage["createAccountSuccess"]));
    const [authorizationExpired, setAuthorizationExpired] = useState(!!(sessionStorage["authorizationExpired"]))
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [loginAs, setLoginAs] = useState(1); // 1 = Cliente | 2 = Empresa

    const [emailMessage, setEmailMessage] = useState("");
    const [passwordMessage, setPasswordMessage] = useState("");


    useEffect(() => { delete sessionStorage["createAccountSuccess"]; }, [createAccountSuccess]);
    useEffect(() => { delete sessionStorage["authorizationExpired"]; }, [authorizationExpired]);

    /**
     * Realizar login do usuário.
     * @returns 
     */
    async function Login() {

        const validate = ValidateInputs();
        if (!validate) { return; }

        setLoading(true);
        setError(null);

        try {
            const result = await Post(loginAs == 1 ? "/customers/login" : "/companies/login", { email, password });
            const { token, expiration, name } = result;
            localStorage["authorization"] = token;
            localStorage["authorization_expiration"] = expiration;
            localStorage["loginAs"] = loginAs;
            localStorage["entityName"] = name;
            window.location.href = "/"; // Ir para ínicio.
        }
        catch (error) {
            setLoading(false);
            setError(error);
        }

    }

    /**
     * Validar campos do login.
     * @returns 
     */
    function ValidateInputs() {

        if (!email) {
            document.getElementById("emailInput").focus();
            setEmailMessage("Digite o e-mail de acesso.");
            return false;
        }

        if (!IsEmail(email)) {
            document.getElementById("emailInput").focus();
            setEmailMessage("O e-mail digitado é inválido.");
            return false;
        }

        if (!password) {
            document.getElementById("passwordInput").focus();
            setPasswordMessage("Digite a senha de acesso.");
            return false;
        }

        return true;
    }


    return (
        <>
            <div className="container">
                <div className="row justify-content-center">
                    <div className="col-6">
                        <div className="card">

                            <div className="card-body">
                                <h5 className="card-title display-6">Login</h5>
                                <p className="card-text">
                                    <span>Insira suas credenciais para acessar o sistema.</span>
                                    {/* <small className="d-block">Não tem uma conta? <a href="/criar-conta">Clique aqui</a> para criar uma.</small> */}
                                </p>


                                <ul className="nav nav-pills nav-fill mb-3">
                                    <li className="nav-item">
                                        <a className={`nav-link ${loginAs == 1 && "active"}`} aria-current="page" onClick={() => setLoginAs(1)} href={"javascript:;"}>
                                            <i className="bi bi-person-fill me-2"></i>
                                            Cliente
                                        </a>
                                    </li>
                                    <li className="nav-item">
                                        <a className={`nav-link nav-link-company ${loginAs == 2 && "active"}`} onClick={() => setLoginAs(2)} href={"javascript:;"}>
                                            <i className="bi bi-building-fill me-1"></i>
                                            Empresa
                                        </a>
                                    </li>
                                </ul>

                                <form>

                                    <div className="mb-3">
                                        <label className="form-label" htmlFor="emailInput">
                                            <i class="bi bi-at me-1"></i>
                                            E-mail
                                        </label>
                                        <input
                                            className="form-control"
                                            id="emailInput"
                                            type="email"
                                            value={email}
                                            onChange={({ target }) => setEmail(target.value)}
                                            disabled={loading}
                                            onKeyUp={({ key }) => {
                                                setEmailMessage("")
                                                key === "Enter" ? document.getElementById("passwordInput").focus() : (() => { })();
                                            }}
                                        />
                                        <div className="form-text">
                                            {emailMessage}
                                        </div>
                                    </div>

                                    <div className="mb-3">
                                        <label className="form-label" htmlFor="passwordInput">
                                            <i class="bi bi-key me-1"></i>
                                            Senha
                                        </label>
                                        <input
                                            className="form-control"
                                            id="passwordInput"
                                            type="password"
                                            value={password}
                                            onChange={({ target }) => setPassword(target.value)}
                                            disabled={loading}
                                            onKeyUp={({ key }) => {
                                                setPasswordMessage("")
                                                key === "Enter" ? Login() : (() => { })();
                                            }}
                                        />
                                        <div className="form-text">
                                            {passwordMessage}
                                        </div>
                                    </div>


                                </form>

                                {
                                    createAccountSuccess &&
                                    <>
                                        <div class="alert bg-success text-white" role="alert">
                                            Sua conta foi criada com êxito!
                                        </div>
                                    </>
                                }

                                {
                                    authorizationExpired &&
                                    <>
                                        <div class="alert bg-warning text-white" role="alert">
                                            Sua sessão expirou, por favor, faça login novamente.
                                        </div>
                                    </>
                                }


                                {
                                    error &&
                                    <>
                                        <div class="alert alert-danger" role="alert">
                                            <ul className="mb-0">
                                                {
                                                    error.messages.map((message) => <li>{message}</li>)
                                                }
                                            </ul>
                                        </div>
                                    </>
                                }

                                <p className="card-text">
                                    <small>
                                        Não tem uma conta? <a href="/criar-conta" style={{ textDecoration: "none" }}>Clique aqui</a> para criar uma.
                                    </small>
                                </p>

                                <button
                                    className={"btn px-5 " + (loginAs == 2 ? "btn-success" : "btn-primary")}
                                    type="button"
                                    style={{ width: "100%" }}
                                    onClick={() => Login()}
                                    disabled={loading}>

                                    {
                                        loading ?
                                            <>
                                                <span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>
                                                Entrando...
                                            </>
                                            :
                                            <>
                                                <i className="bi bi-box-arrow-in-right me-2" />
                                                {loginAs == 2 ? <>Entrar como <strong>Empresa</strong></> : <>Entrar como <strong>Cliente</strong></>}
                                            </>
                                    }

                                </button>

                                <button
                                    className="btn btn-outline-secondary mt-2"
                                    type="button"
                                    style={{ width: "100%" }}
                                    onClick={() => window.location.href = "/"}
                                    disabled={loading}>
                                    <i class="bi bi-arrow-left me-2"></i>
                                    Início
                                </button>


                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </>
    );
}