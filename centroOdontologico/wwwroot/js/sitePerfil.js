const modal = new bootstrap.Modal(document.getElementById("modal"),
    {
        keyboard: false,
        backdrop: "static"
    });
const router = new Ruta().pack;
const btnGuardar = document.getElementById("guardar");




btnGuardar.addEventListener("click", res => {
    guardar();
});





const guardar = async () => {
    const val = await validaciones();



    if (val == "ok") {
        tata.error('', 'Todos los campos son requeridos');

        return;
    }


    let clave = document.getElementById("clave");
    let confirmar = document.getElementById("confirmar")


    if (clave.value != confirmar.value) {

        tata.error('', 'Las contraseñas no coinciden');
        clave.classList.add("is-invalid");
        confirmar.classList.add("is-invalid");

        return;

    }

    const form = document.getElementById('formulario');
    const formulario = new FormData(form);

    formulario.append("idUsuario", _idUsuario);

    const res = await axios.post(`${router}Usuarios/guardar`, formulario);
    
    if (res.data == "ok") {



        tata.success('', 'Perfil editado correctamente, Inicie sesion nuevamente');

            
        await axios.post(`${router}Home/Salir`, formulario);
        
        modal.hide();
        vaciar();

        setTimeout(function () { window.location.reload(); }, 3700);
        
  
     
 
 
 
    } else if (res.data == "repetido") {
        tata.error('', 'Este usuario ya esta registrado');
    } else {
        tata.error('', 'Error al guardar los datos');
    }
}






const editar = async (id) => {



    vaciar();
    document.getElementById("titulo").innerText = "Editar Perfil";
    const formulario = new FormData();
    formulario.append("idUsuario", id);
    let res = await axios.post(`${router}Usuarios/cargarPerfil`, formulario);
    res = res.data;
    _idUsuario = res.idUsuario;
    patchValues(res);
    modal.show();



}



const patchValues = (obj) => {

    Object.keys(obj).forEach((Objeckey) => {


        let element = document.getElementById(`${Objeckey}`);
        if (!!element) element.value = obj[`${Objeckey}`];



    })
   document.getElementById("confirmar").value = document.getElementById("clave").value;   

}



const comboRoles = async () => {


    let res = await axios.post(`${router}Usuarios/comboRoles`);
    res = res.data;
    let cmbCiudad = document.getElementById("idRol");



    cmbCiudad.innerHTML = `<option value="">--Seleccione un Rol--</option>`;

    res.forEach(res => {



        cmbCiudad.innerHTML += `<option value="${res.idRol}">${res.nombre}</option>`;

    });



}
comboRoles();
modal.show();
editar();