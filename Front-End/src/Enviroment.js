export const Enviroment = (() => {
    const hostname = window.location.hostname.toLowerCase();
    const redirectToEnviroment = localStorage["enviroment"] || null;
  
    // REDIRECIONADOR //
    if (redirectToEnviroment) {
      return redirectToEnviroment;
    }
  
    // LOCALHOST //
    if (hostname == "") {
      return "LOCAL";
    }
    if (hostname == "localhost") {
      return "LOCAL";
    }
    if (hostname.includes(".")) {
      return "LOCAL_IP";
    }
    // if (hostname.includes(".")) { return "LOCAL_IP"; }
  
    // DEV //
    if (hostname == "dev") {
      return "DEVELOPMENT";
    }
    if (window.Electron) {
      return "LOCAL";
    }
  
    // TEST //
    if (hostname == "test") {
      return "TEST";
    }
  
    // HOMOLOG //
    if (hostname == "homolog") {
      return "HOMOLOG";
    }
  
    // PROD //
    if (hostname == "prod") {
      return "PRODUCTION";
    }
    return "PRODUCTION";
  })();
  