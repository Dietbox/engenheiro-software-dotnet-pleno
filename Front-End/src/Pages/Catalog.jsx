import { useEffect, useState } from "react";
import Product from "../Components/Product";
import { Get, Post } from "../Request";

export default function CatalogPage(props) {

    const productLoading = { loading: true }
    const { isCompany } = props;
    const [products, setProducts] = useState([productLoading, productLoading, productLoading]);
    const [error, setError] = useState(null);

    const [buyId, setBuyId] = useState(0);
    const [buyLoading, setBuyLoading] = useState(false);


    useEffect(() => {

        GetProducts();

    }, [])

    /**
     * Obter produtos.
     */
    async function GetProducts() {
        try {

            const result = await Get("/products");
            const list = Array.isArray(result) ? result : [];
            setProducts(list);

        } catch (error) {
            console.error("> FALHA AO BUSCAR LISTA DE PRODUTOS", error);
            setError(error);
            setProducts([])
        }
    }

    /**
     * Comprar produto.
     */
    async function BuyProduct(id, name) {

        const amount = prompt(`Quantas unidades do produto '${name}' você quer comprar?`, 1);
        if (!amount) { return }

        setBuyId(id);
        setBuyLoading(true);
        setError(null);

        try {
            await Post(`/products/${id}/buy`, { amount });
            alert(`COMPRA EFETUADA\nVocê comprou ${amount} unidades(s) do produto '${name}' com êxito.\nParabéns!`);
            setBuyLoading(false);
            GetProducts();
        }
        catch (error) {
            console.error("> FALHA AO COMPRAR PRODUTO", error);
            setError(error);
            setBuyLoading(false);
        }

    }

    return (
        <>
            <div className="container-fluid p-5">
                <div className="row">

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
                       !error && products.length == 0 ?
                            <>
                                <div class="alert alert-warning" role="alert">
                                    <ul className="mb-0">
                                        <li>Nenhum produto disponível.</li>
                                    </ul>
                                </div>
                            </>
                            :
                            products.map((product) =>
                                <>
                                    <div className="col-12 col-md-6 col-lg-6 col-xxl-4">
                                        <Product {...product} isCompany={isCompany} BuyProduct={BuyProduct} buyLoading={buyLoading} buyId={buyId} />
                                    </div>
                                </>
                            )
                    }
                </div>
            </div>
        </>
    )
}