export default function Footer() {
    return (
        <>
            <div className="container">
                <footer className="d-flex flex-wrap justify-content-between align-items-center py-3 my-4 border-top">
                    <div className="col-md-4 d-flex align-items-center">
                        <a
                            href="/"
                            className="mb-3 me-2 mb-md-0 text-muted text-decoration-none lh-1"
                        >
                        </a>
                        <span className="mb-3 mb-md-0 text-muted">© 2023 Leonardo Valcarenghi</span>
                    </div>
                    <ul className="nav col-md-4 justify-content-end list-unstyled d-flex">
                        <li className="ms-3">
                            <a className="text-muted" href="https://leonardovalcarenghi.com.br/" target="_blank">
                                <i class="bi bi-globe"></i>
                            </a>
                        </li>
                        <li className="ms-3">
                            <a className="text-muted" href="https://github.com/leonardovalcarenghi" target="_blank">
                                <i class="bi bi-github"></i>
                            </a>
                        </li>
                        <li className="ms-3">
                            <a className="text-muted" href="https://www.linkedin.com/in/leonardo-valcarenghi/" target="_blank">
                                <i class="bi bi-linkedin"></i>
                            </a>
                        </li>
                    </ul>
                </footer>
            </div>

        </>
    );
}