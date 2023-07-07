import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { DecimalToMoney } from "../Utils";
import { Get, Post } from "../Request";

export default function ViewProductPage(props) {

    const { productID } = useParams();
    const { isCompany } = props;
    const [product, setProduct] = useState(null);
    const [success, setSuccess] = useState(null);
    const [error, setError] = useState(null);
    const [buyError, setBuyError] = useState(null);
    const [loading, setLoading] = useState(true);
    const [buyLoading, setBuyLoading] = useState(false);
    const { name, description, brand, price, stock, active } = product || { stock: 1, active: true };

    const FAKE_DESCRIPTION = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam placerat lacinia dolor. Aenean elementum dignissim fringilla. Pellentesque vulputate, diam sed maximus pulvinar, lorem magna tristique nulla, ut pretium est dui vel mi. Etiam ex nunc, tristique vitae felis id, porta lobortis odio. Sed viverra commodo ex, eget commodo lorem aliquam.";


    useEffect(() => {

        GetProduct();


    }, []);

    /**
     * Obter produto.
     */
    async function GetProduct() {

        setLoading(true);
        setError(null);

        try {
            const result = await Get(`/products/${productID}`);
            setProduct(result);
            setLoading(false);
        }
        catch (error) {
            console.error("> FALHA AO BUSCAR PRODUTO", error);
            setError(error);
            setLoading(false);
        }

    }

    /**
     * Comprar produto.
     */
    async function BuyProduct() {

        const amount = prompt(`Quantas unidades do produto '${name}' você quer comprar?`, 1);
        if (!amount) { return }

        setBuyLoading(true);
        setBuyError(null);

        try {
            await Post(`/products/${productID}/buy`, { amount });
            alert(`COMPRA EFETUADA\nVocê comprou ${amount} unidades(s) do produto '${name}' com êxito.\nParabéns!`);
            setBuyLoading(false);
            GetProduct();        
        }
        catch (error) {
            console.error("> FALHA AO COMPRAR PRODUTO", error);
            setBuyError(error);
            setBuyLoading(false);
        }
        
    }

    return (
        error
            ?
            <>
                <div className={"container pt-5"}>
                    <div className="row justify-content-center">
                        <div className="col-10">
                            <div className="p-2">
                                <div className="display-6 mb-0 pb-0">Ocorreu um problema.</div>
                                <ul className="p-3 pl-5">
                                    {error.messages.map(message => <li>{message}</li>)}
                                </ul>

                                <button className="btn btn-outline-secondary" onClick={() => GetProduct()}>
                                    <i class="bi bi-arrow-clockwise me-2"></i>
                                    Tentar Novamente
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </>
            :
            <>



                <div className={"container pt-5 " + (loading && "placeholder-wave")}>
                    <div className="row justify-content-center">

                        {/* Imagem */}
                        <div className="col-5">

                            <div id="carouselExample" className="carousel slide carousel-fade carousel-dark">
                                <div className="carousel-inner">
                                    <div className="carousel-item active">
                                        <img src="https://placehold.co/550x400" className="img-fluid rounded" alt="..." />
                                    </div>
                                    <div className="carousel-item">
                                        <img src="https://placehold.co/550x400/orange/white" className="img-fluid rounded" alt="..." />
                                    </div>
                                    <div className="carousel-item">
                                        <img src="https://placehold.co/550x400/blue/white" className="img-fluid rounded" alt="..." />
                                    </div>
                                </div>
                                <button
                                    className="carousel-control-prev"
                                    type="button"
                                    data-bs-target="#carouselExample"
                                    data-bs-slide="prev"
                                >
                                    <span className="carousel-control-prev-icon" aria-hidden="true" />
                                    <span className="visually-hidden">Previous</span>
                                </button>
                                <button
                                    className="carousel-control-next"
                                    type="button"
                                    data-bs-target="#carouselExample"
                                    data-bs-slide="next"
                                >
                                    <span className="carousel-control-next-icon" aria-hidden="true" />
                                    <span className="visually-hidden">Next</span>
                                </button>
                            </div>


                        </div>

                        {/* Informações */}
                        <div className="col-6">

                            <div>
                                {
                                    loading ?
                                        <>
                                            <span class="placeholder col-8"></span>
                                            <span class="placeholder placeholder-xs col-3 d-block mt-2"></span>
                                        </>
                                        :
                                        <>
                                            <h3 className="pb-0 mb-0">{name || "Lorem ipsum dolor sit amet."}</h3>
                                            <small className="text-muted text-uppercase">{brand || "Lorem ipsum"}</small>
                                        </>
                                }
                            </div>

                            <div className="mt-3">
                                {
                                    loading ?
                                        <>
                                            <span class="placeholder placeholder-xs col-2 d-block" ></span>
                                            <span class="placeholder col-10 mt-1" style={{ minHeight: "7rem" }}></span>
                                        </>
                                        :
                                        <>
                                            <label class="produto-details-label">Descrição</label>
                                            <p>{description || FAKE_DESCRIPTION}</p>
                                        </>
                                }

                            </div>




                            <div className="mt-3">
                                {
                                    loading ?
                                        <>

                                            <span class="placeholder col-4 mt-1" style={{ minHeight: "2rem" }}></span>
                                            <span class="placeholder placeholder-xs col-3 d-block mt-1" ></span>
                                        </>
                                        :
                                        <>
                                            <h2>
                                                <span>
                                                    R$ {price ? DecimalToMoney(price) : "0,00"}
                                                </span>
                                                <small className="d-block text-muted small" style={{ fontSize: "12.8px" }}>
                                                    10x R$ {price ? DecimalToMoney(price / 10) : "0,00"} sem juros.
                                                </small>
                                            </h2>
                                        </>
                                }

                            </div>

                            <div className="mt-3">
                                {
                                    loading ?
                                        <>
                                            <span class="placeholder placeholder-xs col-2 d-block"></span>
                                            <span class="placeholder col-5 mt-1 mb-2" ></span>
                                        </>
                                        :
                                        <>
                                            <label class="produto-details-label">Disponibilidade</label>
                                            {
                                                stock == 0 ? <p>Produto indisponível.</p> : <p>{stock} unidades disponíveis.</p>
                                            }
                                        </>
                                }


                            </div>


                            {
                                buyError &&
                                <>
                                    <div class="alert alert-danger" role="alert">
                                        <ul className="mb-0">
                                            {
                                                buyError.messages.map((message) => <li>{message}</li>)
                                            }
                                        </ul>
                                    </div>
                                </>
                            }

                            <div className="row">

                                <div className="col-12">
                                    {
                                        stock == 0 ?
                                            <>
                                                <button
                                                    className="btn btn-outline-secondary"
                                                    style={{ width: "100%" }}
                                                    onClick={() => { alert("Não Implementado;") }}
                                                    disabled={loading || buyLoading}
                                                >
                                                    <i class="bi bi-send me-2"></i>
                                                    Avise-me quando chegar
                                                </button>
                                            </>
                                            :
                                            <>


                                                {
                                                    !isCompany &&
                                                    <>
                                                        <button
                                                            className={"btn btn-primary " + (loading && "placeholder ")}
                                                            style={{ width: "100%" }}
                                                            onClick={() => { alert("Não Implementado;") }}
                                                            disabled={loading || buyLoading}
                                                        >
                                                            {
                                                                !loading &&
                                                                <>
                                                                    <i className="bi bi-cart-plus-fill me-2"></i>
                                                                    Adicionar ao Carrinho
                                                                </>
                                                            }
                                                        </button>

                                                        <button
                                                            className={"btn btn-success mt-2 " + (loading && "placeholder")}
                                                            style={{ width: "100%" }}
                                                            disabled={loading || buyLoading}
                                                            onClick={BuyProduct}
                                                        >
                                                            {
                                                                buyLoading ?
                                                                    <>
                                                                        <span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>
                                                                        Processando compra...
                                                                    </>
                                                                    :
                                                                    !loading &&
                                                                    <>
                                                                        <i className="bi bi-bag-fill me-2"></i>
                                                                        Comprar Agora
                                                                    </>
                                                            }
                                                        </button>
                                                    </>
                                                }



                                            </>
                                    }


                                    <button
                                        className={"btn btn-outline-secondary mt-2 " + (loading && "placeholder")}
                                        type="button"
                                        style={{ width: "100%" }}
                                        onClick={() => window.location.href = "/"}
                                        disabled={loading || buyLoading}>
                                        {
                                            !loading &&
                                            <>
                                                <i class="bi bi-arrow-left me-2"></i>
                                                Voltar
                                            </>
                                        }
                                    </button>
                                </div>




                                <div className="col-12 mt-2">

                                </div>
                            </div>


                            {/* <button href="#" className="btn btn-primary " disabled={!active} >
                            <i className="bi bi-box-arrow-up-right me-2"></i>
                            Detalhes
                        </button> */}


                        </div>


                    </div>
                </div>



            </>
    );
}