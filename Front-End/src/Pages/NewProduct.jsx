import { useState } from "react"
import { Post } from "../Request";

export default function NewProductPage(props) {

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    // Campos:
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [brand, setBrand] = useState("");
    const [price, setPrice] = useState("");
    const [code, setCode] = useState("");
    const [stock, setStock] = useState("");

    // Mensagens dos Campos:
    const [nameMessage, setNameMessage] = useState("");
    const [descriptionMessage, setDescriptionMessage] = useState("");
    const [brandMessage, setBrandMessage] = useState("");
    const [priceMessage, setPriceMessage] = useState("");
    const [codeMessage, setCodeMessage] = useState("");
    const [stockMessage, setStockMessage] = useState("");

    /**
     * Cadastrar produto.
     * @returns 
     */
    async function CreateProduct() {

        const validate = ValidateInputs();
        if (!validate) { return; }

        setError(null);
        setLoading(true);

        try {

            const result = await Post("/products", { name, description, brand, price, code, stock });
            const { id } = result;
            window.location.href = `/produto/${id}`;
        }
        catch (error) {
            console.error("> FALHA AO CADASTRAR PRODUTO", error);
            setError(error);
            setLoading(false);
        }
    }


    /**
     * Validar campos do login.
     */
    function ValidateInputs() {

        if (!name) {
            document.getElementById("nameInput").focus();
            setNameMessage("Informe o nome do produto.");
            return false;
        }

        if (!brand) {
            document.getElementById("brandInput").focus();
            setBrandMessage("Informe a marca do produto.");
            return false;
        }

        if (!price) {
            document.getElementById("priceInput").focus();
            setPriceMessage("Informe o valor do produto.");
            return false;
        }

        if (!stock) {
            document.getElementById("stockInput").focus();
            setStockMessage("Informe a quantidade disponível em estoque do produto.");
            return false;
        }


        return true;
    }

    return (
        <>       
            <div className="container p-5">
                <div className="row justify-content-center">
                    <div className="col-12 col-lg-6">
                        <div className="card">
                            <div className="card-body">

                                <h5 className="card-title display-6">Cadastrar Produto</h5>
                                <p className="card-text">
                                    <span>Preencha as informações para cadastrar um novo produto.</span>
                                </p>

                                <form>

                                    {/* NOME DO PRODUTO */}
                                    <div className="mb-3">
                                        <label className="form-label" htmlFor="nameInput">
                                            * Nome do Produto
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
                                            placeholder={"Ex. Apple iPhone 14 Pro MAX"}
                                        />
                                        <div className="form-text">
                                            {nameMessage}
                                        </div>
                                    </div>


                                    {/* DESCRIÇÃO DO PRODUTO */}
                                    <div className="mb-3">
                                        <label className="form-label" htmlFor="descriptionInput">
                                            Descrição
                                        </label>
                                        <textarea
                                            className="form-control"
                                            id="descriptionInput"
                                            type="text"
                                            style={{ height: "138px", minHeight: "60px", maxHeight: "258px" }}
                                            maxLength={400}
                                            value={description}
                                            onChange={({ target }) => setDescription(target.value)}
                                            onKeyUp={() => setDescriptionMessage("")}
                                            disabled={loading}
                                            placeholder={"Ex. Uma nova forma de interação no seu iPhone.\nUm recurso essencial de segurança projetado para salvar vidas.\nCâmera grande-angular inovadora de 48 MP.\nTela duas vezes mais brilhante sob a luz do sol◊. Tudo com a potência do chip para smartphone que é o máximo."}
                                        />
                                        <div className="form-text">
                                            {descriptionMessage}
                                        </div>
                                    </div>

                                    {/* MARCA DO PRODUTO */}
                                    <div className="mb-3">
                                        <label className="form-label" htmlFor="brandInput">
                                            * Marca
                                        </label>
                                        <input
                                            className="form-control"
                                            id="brandInput"
                                            type="text"
                                            value={brand}
                                            maxLength={50}
                                            onChange={({ target }) => setBrand(target.value)}
                                            onKeyUp={() => setBrandMessage("")}
                                            disabled={loading}
                                            placeholder={"Ex. Apple"}
                                        />
                                        <div className="form-text">
                                            {brandMessage}
                                        </div>
                                    </div>

                                    {/* VALOR DO PRODUTO */}
                                    <div className="mb-3">
                                        <label className="form-label" htmlFor="priceInput">
                                            * Valor
                                        </label>

                                        <div className="input-group mb-3">
                                            <span className="input-group-text" id="basic-addon1">
                                                R$
                                            </span>
                                            <input
                                                className="form-control"
                                                id="priceInput"
                                                type="number"
                                                placeholder={"Ex. 7.998,89"}
                                                onChange={({ target }) => setPrice(target.value)}
                                                onKeyUp={() => setPriceMessage("")}
                                                disabled={loading}
                                                aria-label="Username"
                                                aria-describedby="basic-addon1"
                                            />
                                        </div>




                                        <div className="form-text">
                                            {priceMessage}
                                        </div>
                                    </div>

                                    {/* CÓDIGO DO PRODUTO */}
                                    <div className="mb-3">
                                        <label className="form-label" htmlFor="codeInput">
                                            Código
                                        </label>
                                        <input
                                            className="form-control"
                                            id="codeInput"
                                            type="text"
                                            value={code}
                                            maxLength={50}
                                            onChange={({ target }) => setCode(target.value)}
                                            onKeyUp={() => setCodeMessage("")}
                                            disabled={loading}
                                            placeholder={"Ex. APPLE-PHONE-14-MAX-254GB"}
                                        />
                                        <div className="form-text">
                                            {codeMessage}
                                        </div>
                                    </div>

                                    {/* ESTOQUE DO PRODUTO */}
                                    <div className="mb-3">
                                        <label className="form-label" htmlFor="stockInput">
                                            * Estoque Disponível
                                        </label>
                                        <input
                                            className="form-control"
                                            id="stockInput"
                                            type="number"
                                            value={stock}
                                            onChange={({ target }) => setStock(target.value)}
                                            onKeyUp={() => setStockMessage("")}
                                            disabled={loading}
                                            placeholder={"Ex. 01"}
                                        />
                                        <div className="form-text">
                                            {stockMessage}
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


                                <button
                                    className={"btn btn-primary px-5 "}
                                    type="button"
                                    style={{ width: "100%" }}
                                    onClick={() => CreateProduct()}
                                    disabled={loading}>

                                    {
                                        loading ?
                                            <>
                                                <span className="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>
                                                Cadastrando...
                                            </>
                                            :
                                            <>
                                                <i className="bi bi-plus-square me-2"></i>
                                                Cadastrar
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
    )
}