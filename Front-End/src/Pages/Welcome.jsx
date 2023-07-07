import { useEffect, useState } from "react";
import Product from "../Components/Product";
import "./../WelcomeEffect.css"
export default function WelcomePage() {






    return (
        <>

            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />
            <div className="particle" />

            <div className="d-flex align-items-center" style={{ height: "90vh" }}>



                <div style={{ width: "100%" }}>
                    <h1 className="display-4" >
                        <i class="bi bi-box-seam me-3" style={{ color: "#05e279" }} />
                        <span style={{ color: "#05e279" }}>Dietbox</span> <strong style={{ color: "#6828ED" }}>e-Commerce</strong>
                    </h1>
                    <p className="lead">Desenvolvido por Leonardo Valcarenghi</p>
                    <hr className="my-4" />
                    <p>O que você gostaria de fazer?</p>
                    <p className="lead">

                        <a className="btn btn-primary btn-lg px-5 me-3" href="/login">
                            <i className="bi bi-box-arrow-in-right me-2" />
                            Fazer Login
                        </a>


                        <a className="btn btn-secondary btn-lg px-4" type="button" href="/criar-conta">
                            <i className="bi bi-plus-circle me-2" />
                            Criar Conta
                        </a>


                    </p>

                </div>

            </div>


        </>
    );
}