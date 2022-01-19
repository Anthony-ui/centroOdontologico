const btnGuardar = document.getElementById("guardar");
const btnSalir = document.getElementById("salir");
const btnNuevo = document.getElementById("nuevo");
const correo = document.getElementById("correo");
const tlfono = document.getElementById("telefono");
const router = new Ruta().pack;
const cuerpoTabla = document.querySelector("tbody");
const modal = new bootstrap.Modal(document.getElementById("modal"),
    {
        keyboard: false,
        backdrop: "static"
    });

var _idDoctor = 0;
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

correo.addEventListener("keyup", res => {
    email(correo.value);
});

tlfono.addEventListener("keyup", res => {
    telefono(tlfono.value)
});

const guardar = async () => {

    const val = await validaciones();

    if (val == "ok") {
        tata.error('', 'Todos los campos son requeridos');

        return;
    }
    const form = document.getElementById('formulario');
    const formulario = new FormData(form);

    formulario.append("idDoctor", _idDoctor);

    const res = await axios.post(`${router}Doctores/guardar`, formulario);

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
    const res = await axios.post(`${router}Doctores/listar`);



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

                  <td class='text-center'>${res.cedula}</td>

                  <td class='text-center'>${res.nombres + " " + res.apellidos}</td>

                  <td class='text-center'>${res.direccion}</td>

                  <td class='text-center'>${res.correo}</td>

                  <td class='text-center'>${res.telefono}</td>

                 <td class='text-center'>${res.idEspecialidadNavigation.nombre}</td>

                 <td class='text-center'>

                <div class="dropdown">
                <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
                Opciones
                </button>
                <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                <li><a class="dropdown-item" onclick="editar(${res.idDoctor})">Editar</a></li>
                <li><a class="dropdown-item" onclick="eliminar(${res.idDoctor})">Eliminar</a></li>
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
    formulario.append("idDoctor", id);
    let res = await axios.post(`${router}Doctores/cargar`, formulario);
    res = res.data;
    _idDoctor = res.idDoctor;
    patchValues(res);
    modal.show();



}




const patchValues = (obj) => {

    Object.keys(obj).forEach((Objeckey) => {

        let element = document.getElementById(`${Objeckey}`);
        if (!!element) element.value = obj[`${Objeckey}`];
    })
}



const eliminar = async (id) => {



    if (confirm("El registro se eliminara de manera permanente")) {


        const formulario = new FormData();
        formulario.append("idDoctor", id);
        const url = `${router}Doctores/eliminar`
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

const comboCiudades = async () => {


    let res = await axios.post(`${router}Doctores/comboCiudades`);
    res = res.data;
    let cmbCiudad = document.getElementById("idCiudad");



    cmbCiudad.innerHTML = `<option value="">--Seleccione una Ciudad--</option>`;

    res.forEach(res => {


        cmbCiudad.innerHTML += `<option value="${res.idCiudad}">${res.nombre}</option>`;

    });



}



const comboEspecialidades = async () => {


    let res = await axios.post(`${router}Doctores/comboEspecialidades`);
    res = res.data;
    let cmbCiudad = document.getElementById("idEspecialidad");



    cmbCiudad.innerHTML = `<option value="">--Seleccione una Especialidad--</option>`;

    res.forEach(res => {


        cmbCiudad.innerHTML += `<option value="${res.idEspecialidad}">${res.nombre}</option>`;

    });



}



listar();
comboCiudades();
comboEspecialidades();