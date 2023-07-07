import { API_URL } from "./Enums";
import { Enviroment } from "./Enviroment";
import { IsJSON } from "./Utils";

const SUCCESSFUL_RESPONSES_STATUS_CODES = [200, 201, 202, 204];
const CLIENT_ERROR_RESPONSES_STATUS_CODES = [
  400, 403, 404, 405, 406, 408, 409, 411, 413, 414, 415, 429,
];

const AvailableMethods = [
  "GET",
  "POST",
  "PATCH",
  "DELETE",
  "PUT",
  "FORM",
  "OPTIONS",
];

/**
 * Realizar uma solicitação 'GET' para uma API específica.
 * @param {string} [url] URL de chamada da API.
 * @return {Promise<*>} Retorna uma 'Promise' de solicitação na API (em caso de erro, obtê-lo por TryCatch).
 */
export const Get = async (url = "") => await CreateHttpRequest("GET", url);

/**
 * Realizar uma solicitação 'POST' para uma API específica.
 * @param {string} [url] URL de chamada da API.
 * @param {object} parameters Parâmetros de envio (JSON).
 * @return {Promise<*>} Retorna uma 'Promise' de solicitação na API (em caso de erro, obtê-lo por TryCatch).
 */
export const Post = async (url = "", parameters) =>
  await CreateHttpRequest("POST", url, parameters);

/**
 * Realizar uma solicitação 'PATCH' para uma API específica.
 * @param {string} [url] URL de chamada da API.
 * @param {object} parameters Parâmetros de envio (JSON).
 * @return {Promise<*>} Retorna uma 'Promise' de solicitação na API (em caso de erro, obtê-lo por TryCatch).
 */
export const Patch = async (url = "", parameters) =>
  await CreateHttpRequest("PATCH", url, parameters);

/**
 * Realizar uma solicitação 'PUT' para uma API específica.
 * @param {string} [url] URL de chamada da API.
 * @param {object} parameters Parâmetros de envio (JSON).
 * @return {Promise<*>} Retorna uma 'Promise' de solicitação na API (em caso de erro, obtê-lo por TryCatch).
 */
export const Put = async (url = "", parameters) =>
  await CreateHttpRequest("PUT", url, parameters);

/**
 * Realizar uma solicitação 'DELETE' para uma API específica.
 * @param {string} [url] URL de chamada da API.
 * @param {object} parameters Parâmetros de envio (JSON).
 * @return {Promise<*>} Retorna uma 'Promise' de solicitação na API (em caso de erro, obtê-lo por TryCatch).
 */
export const Delete = async (url = "", parameters) =>
  await CreateHttpRequest("DELETE", url, parameters);

async function CreateHttpRequest(method = "", url = "", parameters) {
  const validMethod = AvailableMethods.includes(method);
  if (!validMethod) {
    throw {
      error: true,
      message: "Método HTTP inválido.",
    };
  }

  const isOline = window.navigator.onLine;
  if (!isOline) {
    throw {
      error: true,
      title: "Sem conexão com a internet",
      message:
        "Verifique se seu computador está conectado com a internet e tente novamente.",
    };
  }

  const api = API_URL[Enviroment] + url;
  const authorization = localStorage["authorization"];
  const httpRequest = new XMLHttpRequest();

  const request = await new Promise((resolve) => {
    httpRequest.onreadystatechange = (event) => {
      if (httpRequest.readyState == httpRequest.DONE) {
        const { status, responseText } = httpRequest;
        const response = IsJSON(responseText) ? JSON.parse(responseText) : {};

        if (status == 0) {
          resolve({
            error: true,
            title: "Serviço indisponível",
            messages: ["Não foi possível realizar uma conexão com o servidor."],
            code: "ERR_CONNECTION_REFUSED",
          });
        }

        if (status == 401) {
          delete localStorage["authorization"];
          sessionStorage["authorizationExpired"] = "authorizationExpired";
          window.location.href = "/login";
        }

        if (SUCCESSFUL_RESPONSES_STATUS_CODES.includes(status)) {
          return resolve({ error: false, status, ...response });
        }

        if (CLIENT_ERROR_RESPONSES_STATUS_CODES.includes(status)) {
          return resolve({ error: true, status, ...response });
        }

        resolve({ status, ...response });
      }
    };

    httpRequest.onerror = (event) => {
      resolve({
        error: true,
        title: "Serviço indisponível",
        messages: ["Não foi possível realizar uma conexão com o servidor."],
        code: "CONNECTION_ERROR",
      });
    };

    httpRequest.open(method, api, true);
    httpRequest.setRequestHeader("content-type", "application/json");
    httpRequest.setRequestHeader("authorization", `Bearer ${authorization}`);
    httpRequest.send(method == "GET" ? null : JSON.stringify(parameters));
  });

  const { error, result } = request;
  if (error) {
    throw request || {};
  }
  return result || {};
}
