import { act } from "@testing-library/react";
import { DecimalToMoney } from "../Utils";

export default function Product(props) {

    const { isCompany, BuyProduct, buyLoading, buyId } = props;
    const { id, name, description, brand = "", stock, price, active, loading } = props;

    return (
        loading ?
            <>
                <div className={"card ps-0 m-2 " + (!active && "card-disabled")} product-id={id} key={id}>
                    <div className="row g-0 align-items-center">
                        <div className="col-md-4">
                            <svg class="bd-placeholder-img card-img-top rounded-start" width="100%" height="300px" xmlns="http://www.w3.org/2000/svg" role="img" aria-label="Placeholder" preserveAspectRatio="xMidYMid slice" focusable="false"><title>Placeholder</title><rect width="100%" height="100%" fill="#868e96"></rect></svg>
                        </div>
                        <div className="col-md-8">
                            <div className="card-body">


                                <div className="placeholder-wave">
                                    <h5 class="card-title ">
                                        <span class="placeholder col-10"></span>
                                    </h5>
                                    <span class="placeholder col-2 placeholder-xs" style={{ marginTop: "-20px" }}></span>
                                </div>



                                <p class="card-text placeholder-wave">
                                    <span class="placeholder col-12 placeholder-sm"></span>
                                </p>

                                <p class="card-text placeholder-wave">
                                    <span class="placeholder col-6 placeholder-xs"></span>
                                </p>

                                <h5 className="placeholder-wave mb-3">
                                    <span class="placeholder col-5"></span>
                                    <span class="placeholder placeholder-xs col-3 d-block mt-1"></span>
                                </h5>

                                <a class="btn btn-success disabled placeholder col-4 me-2"></a>
                                <a class="btn btn-primary disabled placeholder col-4"></a>

                            </div>

                        </div>
                    </div>
                </div>

            </>
            :
            <>
                <div className={"card ps-0 m-2 " + (!active && "card-disabled")} product-id={id} key={id}>
                    <div className="row g-0 align-items-center">
                        <div className="col-md-4">
                            <img src="https://placehold.co/400x600" className="img-fluid rounded-start" alt="..." />
                        </div>
                        <div className="col-md-8">
                            <div className="card-body">

                                <div>
                                    <h5 className="card-title mb-0">{name}</h5>
                                    <small className="text-muted text-uppercase">{brand || "Lorem ipsum"}</small>
                                </div>


                                <p className="card-text mt-3" style={{
                                    overflow: "hidden",
                                    textOverflow: "ellipsis",
                                    height: 30,
                                    whiteSpace: "nowrap"
                                }}>
                                    {description}
                                </p>
                                <p className="card-text">
                                    <small className="text-muted">
                                        {stock || 0} unidades disponíveis.
                                    </small>
                                </p>

                                <h5>
                                    <span>
                                        R$ {price ? DecimalToMoney(price) : "0,00"}
                                    </span>
                                    <small className="d-block text-muted small" style={{ fontSize: "12.8px" }}>
                                        10x R$ {price ? DecimalToMoney(price / 10) : "0,00"} sem juros.
                                    </small>
                                </h5>

                                {
                                    !isCompany &&
                                    <button className="btn btn-success me-2" disabled={stock == 0 || !active || buyLoading} onClick={() => BuyProduct(id, name)}>

                                        {
                                            buyId == id && buyLoading ?
                                                <>
                                                    <span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>
                                                    Comprando...
                                                </>
                                                :
                                                <>
                                                    <i className="bi bi-bag-fill me-2"></i>
                                                    {stock == 0 ? "Indisponível" : "Comprar"}
                                                </>
                                        }

                                    </button>
                                }


                                <button className="btn btn-primary " disabled={!active || buyLoading} onClick={() => window.location.href = `/produto/${id}`}>
                                    <i className="bi bi-box-arrow-up-right me-2"></i>
                                    Detalhes
                                </button>
                            </div>

                        </div>
                    </div>
                </div>

            </>
    );
}