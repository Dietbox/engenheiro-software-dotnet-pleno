export default function NavBar({ isCompany, entityName }) {

    const { confirm } = window;

    /**
     * Fazer logout do usuário.
     */
    function Logout() {
        const confirmLogout = confirm("SAIR DO SISTEMA\nTem certeza que deseja sair?");
        if (confirmLogout) {
            delete localStorage["authorization"];
            window.location.href = "/";
        }
    }


    return (
        <>
            <nav className={"navbar navbar-expand-lg  navbar-dark fixed-top " + (isCompany ? "bg-success" : "bg-primary")}>
                <div className="container-fluid">
                    <a className="navbar-brand py-0" href="/">
                        Dietbox e-Commerce
                        {
                            isCompany &&
                            <small className="d-block fst-italic" style={{ fontSize: "12px", marginTop: "-6px" }}>
                                Admin
                            </small>
                        }
                    </a>
                    <button
                        className="navbar-toggler"
                        type="button"
                        data-bs-toggle="collapse"
                        data-bs-target="#navbarSupportedContent"
                        aria-controls="navbarSupportedContent"
                        aria-expanded="false"
                        aria-label="Toggle navigation"
                    >
                        <span className="navbar-toggler-icon" />
                    </button>
                    <div className="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul className="navbar-nav ms-auto mb-2 mb-lg-0">

                            <li className="nav-item">
                                <a className="nav-link text-white" href="/">
                                    <i className="bi bi-house-door-fill me-1" />
                                    Início
                                </a>
                            </li>

                            {/* Exibir somente se login for de empresa. */}
                            {
                                isCompany &&
                                <>
                                    <li className="nav-item dropdown">
                                        <a className="nav-link dropdown-toggle text-white" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                            <i className="bi bi-box-seam-fill me-1" />
                                            Produtos
                                        </a>
                                        <ul className="dropdown-menu dropdown-menu-end">
                                            <li><a className="dropdown-item pe-5" href="/produtos/cadastro">
                                                <i className="bi bi-plus-square me-2"></i>
                                                Cadastrar Produto
                                            </a></li>
                                        </ul>
                                    </li>
                                </>
                            }

                            <li className="nav-item dropdown">
                                <a className="nav-link dropdown-toggle text-white" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                    <i className={"bi me-1 " + (isCompany ? "bi-building-fill" : "bi-person-fill")} />
                                    {entityName}
                                </a>
                                <ul className="dropdown-menu dropdown-menu-end">
                                    <li>
                                        <a className="dropdown-item" href="javascript:;" onClick={Logout}>
                                            <i className="bi bi-door-closed me-2"></i>
                                            Sair
                                        </a>
                                    </li>
                                </ul>
                            </li>

                        </ul>
                    </div>
                </div>
            </nav>

        </>
    )
}