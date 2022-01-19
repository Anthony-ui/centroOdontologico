const router = new Ruta().pack;
const btnLogin = document.getElementById('login');

btnLogin.addEventListener("click", () => {
    login();
});

const login = async () => {
   

    const val = await validaciones();

    if (val == "ok") {
        tata.error('', 'Todos los campos son requeridos');
        return;
    }


    btnLogin.innerText = "Cargando...";
    btnLogin.disabled = true;

    const formulario = new FormData(document.getElementById('formulario'));
    const login = await axios.post(`${router}Home/login`, formulario);

    if (login.data == "ok") {
        location.href = "../../Home/redireccion"
    } else {
        tata.text('', 'Credenciales Incorrectas');
        btnLogin.innerText = "Iniciar Sesion";
        btnLogin.disabled = false;

        
        /*tata.text('Hello World', 'CSSScript.Com')*/
        //tata.info('Hello World', 'CSSScript.Com')
        //tata.success('Hello World', 'CSSScript.Com')
        //tata.warning('Hello World', 'CSSScript.Com')
        //tata.error('Hello World', 'CSSScript.Com')
    }
}