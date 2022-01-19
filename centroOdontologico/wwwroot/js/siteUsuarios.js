const btnGuardar = document.getElementById("guardar");
const btnSalir = document.getElementById("salir");
const btnNuevo = document.getElementById("nuevo");
const correo = document.getElementById("correo");
const router = new Ruta().pack;
const cuerpoTabla = document.querySelector("tbody");
const modal = new bootstrap.Modal(document.getElementById("modal"),
    {
        keyboard: false,
        backdrop: "static"
    });

var _idUsuario = 0;
var _cedulaAnterior = "";


btnGuardar.addEventListener("click", res => {
    guardar();
});

btnSalir.addEventListener("click", res => {
    vaciar();
});

btnNuevo.addEventListener("click", res => {
    vaciar();
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
        tata.success('', 'Datos guardados correctamente');
        vaciar();
        modal.hide();
        listar();
    } else if (res.data == "repetido") {
        tata.error('', 'Este usuario ya esta registrado');
    } else {
        tata.error('', 'Error al guardar los datos');
    }
}

const listar = async () => {
    cuerpoTabla.innerHTML = "";
    let cont = 0;
    let tabla = "";
    const res = await axios.post(`${router}Usuarios/listar`);



    if (res.data.length == 0) {

        tabla = `<tr>


                   <td colspan='8' class='text-center'>

                       No Existen Registros
                      
                   </td>

        
                </tr> `;

    }

    res.data.forEach(res => {
        cont = cont + 1;

        tabla += `<tr>

                  <td class='text-center'>${cont}</td>

                  <td class='text-center'>${res.nombre}</td>

                  <td class='text-center'>${res.fechaRegistro}</td>

                  <td class='text-center'>${res.usuario}</td>

                  <td class='text-center'>${res.idRolNavigation.nombre}</td>

               

                 <td class='text-center'>

                <div class="dropdown">
                <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
                Opciones
                </button>
                <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                <li><a class="dropdown-item" onclick="editar(${res.idUsuario})">Editar</a></li>
                <li><a class="dropdown-item" onclick="eliminar(${res.idUsuario})">Eliminar</a></li>
                </ul>
                </div>

                 </td>

                 </tr>`;
    });

    if (!tabla == "") {
        cuerpoTabla.innerHTML = tabla;
    }
}

const editar = async (id) => {



    vaciar();
    document.getElementById("titulo").innerText = "Editar";
    const formulario = new FormData();
    formulario.append("idUsuario", id);
    let res = await axios.post(`${router}Usuarios/cargar`, formulario);
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

    console.log(document.getElementById("confirmar").value = document.getElementById("clave").value);
}



const eliminar = async (id) => {




    if (confirm("El registro se eliminara de manera permanente")) {


        const formulario = new FormData();
        formulario.append("idUsuario", id);
        const url = `${router}Usuarios/eliminar`
        let res = await axios.post(url, formulario);
        res = res.data;


        if (res == "ok") {

            await listar();
            tata.success('', 'Eliminado Correctamente');

        } else {

            tata.error('', 'Error al eliminar');

        }

    }

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







listar();
comboRoles();
