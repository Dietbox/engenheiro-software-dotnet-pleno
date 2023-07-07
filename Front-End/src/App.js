import { useState, useEffect } from "react";
import {
  createBrowserRouter,
  Navigate,
  RouterProvider,
} from "react-router-dom";
import "./App.css";

// Páginas Públicas:
import WelcomePage from "./Pages/Welcome";
import LoginPage from "./Pages/Login";
import RegisterPage from "./Pages/Register";

// Páginas Privadas:
import ViewProductPage from "./Pages/ViewProduct";
import CatalogPage from "./Pages/Catalog";

// Componentes:
import NavBar from "./Components/NavBar";
import Footer from "./Components/Footer";
import Error404Page from "./Pages/Error404";
import NewProductPage from "./Pages/NewProduct";

export default function App() {

  const [entityName, setEntityName] = useState(localStorage["entityName"]);
  const [isAuth, setIsAuth] = useState(!!localStorage["authorization"]);
  const [isCompany, setIsCompany] = useState(localStorage["loginAs"] == 2);



  const PrivateRoutes = createBrowserRouter([
    {
      path: "/",
      element: <CatalogPage isCompany={isCompany} />,
      errorElement: <Error404Page />,
    },
    {
      path: "produto/:productID",
      element: <ViewProductPage isCompany={isCompany} />,
    },
    {
      path: "produtos/cadastro",
      element: <NewProductPage />,
    },
    {
      path: "criar-conta",
      element: <Navigate to="/" replace={true} />,
    },
    {
      path: "login",
      element: <Navigate to="/" replace={true} />,
    },
  ]);

  const PublicRoutes = createBrowserRouter([
    {
      path: "/",
      element: <WelcomePage />,
      errorElement: <Error404Page />,
    },
    {
      path: "criar-conta",
      element: <RegisterPage />,
    },
    {
      path: "login",
      element: <LoginPage setIsAuth={setIsAuth}  />,
    },
  ]);

  return (
    <>
      {isAuth && <NavBar isCompany={isCompany} entityName={entityName} />}

      <div className="p-5">
        <RouterProvider router={isAuth ? PrivateRoutes : PublicRoutes} />
      </div>

      {isAuth && <Footer />}
    </>
  );
}
