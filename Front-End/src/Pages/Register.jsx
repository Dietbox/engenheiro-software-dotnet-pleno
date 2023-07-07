import { useEffect, useState } from "react";
import ReCAPTCHA from "react-google-recaptcha";
import { IsEmail } from "../Utils";
import { Get, Post } from "../Request";

export default function RegisterPage() {

    const recaptchaKey = "6Lf9WvUmAAAAAIvHWyhXpDTzoCT1llAqqSIrQ5l5";
    const [enabledRecaptcha, setEnabledRecaptcha] = useState(false);
    const [recaptchaToken, setRecaptchaToken] = useState('');

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    // Campos:
    const [createAs, setCreateAs] = useState(1); // 1 = Cliente | 2 = Empresa
    const [name, setName] = useState("");
    const [cnpj, setCnpj] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    // Mensagens dos Campos:
    const [nameMessage, setNameMessage] = useState("");
    const [cnpjMessage, setCnpjMessage] = useState("");
    const [emailMessage, setEmailMessage] = useState("");
    const [passwordMessage, setPasswordMessage] = useState("");

    useEffect(() => {
        CheckRecaptcha();
    }, [])

    /**
     * Verificar se o recaptcha está habilitada para uso.
     */
    async function CheckRecaptcha() {
        try {
            const result = await Get("/public/recaptcha");
            const { enabled } = result;
            setEnabledRecaptcha(enabled);
        }
        catch (error) {

        }
    }

    /**
     * Criar conta.
     */
    async function CreateAccount() {

        const validate = ValidateInputs();
        if (!validate) { return; }

        setLoading(true);
        setError(null);

        try {
            const result = await Post(createAs == 1 ? "/customers/create-account" : "/companies/create-account", { name, cnpj, email, password, recaptcha: recaptchaToken });
            sessionStorage["createAccountSuccess"] = "true";
            window.location.href = "/login";
        }
        catch (error) {
            setLoading(false);
            setError(error);
        }
    }

    /**
     * Validar campos do cadastro.
     */
    function ValidateInputs() {

        if (!name) {
            document.getElementById("nameInput").focus();
            setNameMessage(createAs == 2 ? "Digite o nome da empresa." : "Digite o seu nome.");
            return false;
        }

        if (createAs == 2 && !cnpj) {
            document.getElementById("cnpjInput").focus();
            setCnpjMessage("Digite o CNPJ da empresa.");
            return false;
        }

        if (!email) {
            document.getElementById("emailInput").focus();
            setEmailMessage(createAs == 2 ? "Digite o e-mail da empresa." : "Digite o seu e-mail.");
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
                        <div className="card" >

                            <div className="card-body">
                                <h5 className="card-title display-6">Criar Conta</h5>
                                <p className="card-text">Insira suas informações para criar uma nova conta.</p>



                                <ul className="nav nav-pills nav-fill mb-3">
                                    <li className="nav-item">
                                        <a className={`nav-link ${createAs == 1 && "active"}`} aria-current="page" onClick={() => setCreateAs(1)} href={"javascript:;"}>
                                            <i className="bi bi-person-fill me-2"></i>
                                            Sou Cliente
                                        </a>
                                    </li>
                                    <li className="nav-item">
                                        <a className={`nav-link nav-link-company ${createAs == 2 && "active"}`} onClick={() => setCreateAs(2)} href={"javascript:;"}>
                                            <i className="bi bi-building-fill me-1"></i>
                                            Sou Empresa
                                        </a>
                                    </li>
                                </ul>



                                <form>

                                    {/* NOME */}
                                    <div className="mb-3">
                                        <label className="form-label" htmlFor="nameInput">
                                            <i className="bi bi-card-text me-1"></i>
                                            {createAs == 2 ? "Nome da Empresa" : "Nome"}
                                        </label>
                                        <input
                                            className="form-control"
                                            id="nameInput"
                                            type="text"
                                            maxLength={100}
                                            value={name}
                                            onChange={({ target }) => setName(target.value)}
                                            onKeyUp={() => setNameMessage("")}
                                            disabled={loading}
                                        />
                                        <div className="form-text">
                                            {nameMessage}
                                        </div>
                                    </div>


                                    {/* CNPJ (se for empresa) */}
                                    {
                                        createAs == 2 &&
                                        <>
                                            <div className="mb-3">
                                                <label className="form-label" htmlFor="cnpjInput">
                                                    <i class="bi bi-building me-1"></i>
                                                    CNPJ
                                                </label>
                                                <input
                                                    className="form-control"
                                                    id="cnpjInput"
                                                    type="text"
                                                    maxLength={14}
                                                    value={cnpj}
                                                    onChange={({ target }) => setCnpj(target.value)}
                                                    onKeyUp={() => setCnpjMessage("")}
                                                    disabled={loading}
                                                />
                                                <div className="form-text">
                                                    {cnpjMessage}
                                                </div>
                                            </div>
                                        </>
                                    }

                                    {/* E-MAIL */}
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
                                            onKeyUp={() => setEmailMessage("")}
                                            disabled={loading}
                                        />
                                        <div className="form-text">
                                            {emailMessage}
                                        </div>
                                    </div>

                                    {/* SENHA */}
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
                                            onKeyUp={() => setPasswordMessage("")}
                                            disabled={loading}
                                        />
                                        <div className="form-text">
                                            {passwordMessage}
                                        </div>
                                    </div>



                                </form>

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

                                {
                                    enabledRecaptcha &&
                                    <>
                                        <div className="d-flex justify-content-center my-2">
                                            <ReCAPTCHA
                                                sitekey={recaptchaKey}
                                                onChange={(value) => setRecaptchaToken(value)}
                                            />
                                        </div>
                                    </>
                                }

                                <p className="card-text">
                                    <small>Já tem uma conta? <a href="/login" style={{ textDecoration: "none" }}>Clique aqui</a> para fazer login.</small>
                                </p>

                                <button
                                    className={"btn px-5 " + (createAs == 2 ? "btn-success" : "btn-primary")}
                                    type="button"
                                    style={{ width: "100%" }}
                                    onClick={() => CreateAccount()}
                                    disabled={loading || (enabledRecaptcha && !recaptchaToken)}>

                                    {
                                        loading ?
                                            <>
                                                <span className="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>
                                                Criando conta...
                                            </>
                                            :
                                            <>
                                                <i className={"bi me-2 " + (createAs == 2 ? "bi-building-fill-add" : "bi-person-fill-add")} />
                                                {createAs == 2 ? <>Criar conta de <strong>Empresa</strong></> : <>Criar conta de <strong>Cliente</strong></>}
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