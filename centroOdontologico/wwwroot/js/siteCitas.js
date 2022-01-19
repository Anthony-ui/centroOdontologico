const btnGuardar = document.getElementById("guardar");
const btnSalir = document.getElementById("salir");
const btnNuevo = document.getElementById("nuevo");
const correo = document.getElementById("correo");
const tlfono = document.getElementById("telefono");
const fechCita = document.getElementById("fechaCita");
const router = new Ruta().pack;
const cuerpoTabla = document.querySelector("tbody");
const modal = new bootstrap.Modal(document.getElementById("modal"),
    {
        keyboard: false,
        backdrop: "static"
    });

var _idCita = 0;
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

//correo.addEventListener("keyup", res => {
//    email(correo.value);
//});

//tlfono.addEventListener("keyup", res => {
//    telefono(tlfono.value)
//});

const guardar = async () => {
    const val = await validaciones();

    if (val == "ok") {
        tata.error('', 'Todos los campos son requeridos');

        return;
    }
    const form = document.getElementById('formulario');
    const formulario = new FormData(form);

    formulario.append("idCita", _idCita);

    const res = await axios.post(`${router}Citas/guardar`, formulario);

    if (res.data == "ok") {
        tata.success('', 'Datos guardados correctamente');
        vaciar();
        modal.hide();
        listar();
    } else if (res.data == "repetido") {

        tata.error('', 'Este usuario ya esta registrado');

    } else if (res.data == "menor") {

        tata.error('', 'Seleccione una fecha valida');
        fechCita.classList.add("is-invalid");

    } else if (res.data == "agendada") {

        tata.error('', 'Este usuario ya tiene una cita agendada');

    }

    else {
        tata.error('', 'Error al guardar los datos');
    }
}

const listar = async () => {
    cuerpoTabla.innerHTML = "";
    let cont = 0;
    let tabla = "";
    const res = await axios.post(`${router}Citas/listar`);





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

                  <td class='text-center'>${res.fechaCita}</td>
 
                  <td class='text-center'>${res.idPacienteNavigation.nombres}</td>

                   

                  <td class='text-center'>${res.idDoctorNavigation.nombres + " " + res.idDoctorNavigation.apellidos}</td>


                ${(res.estado == 0) ? `<td class='text-center text-warning'>Pendiente</td>`

                : (res.estado == 1) ? `<td class='text-center text-success'>Atendido</td>`
                : (res.estado == 2) ? `<td class='text-center text-danger'> Cancelado</td>` : res.estado


            }
                   
                

                  

                 

           

                 <td class='text-center'>

                <div class="dropdown">
                <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
                Opciones
                </button>
                <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                <li><a class="dropdown-item" onclick="editar(${res.idCita})">Editar</a></li>
                <li><a class="dropdown-item" onclick="eliminar(${res.idCita})">Eliminar</a></li>
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
    formulario.append("idCita", id);
    let res = await axios.post(`${router}Citas/Cargar`, formulario);
    res = res.data;
    _idCita = res.idCita;
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
        formulario.append("idCita", id);
        const url = `${router}Citas/eliminar`
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

const comboPaciente = async () => {


    let res = await axios.post(`${router}Citas/comboPacientes`);
    res = res.data;
    let cmbCiudad = document.getElementById("idPaciente");



    cmbCiudad.innerHTML = `<option value="">--Seleccione una Paciente--</option>`;

    res.forEach(res => {


        cmbCiudad.innerHTML += `<option value="${res.idPaciente}">${res.cedula + "-" + res.nombres}</option>`;

    });



}


const comboDoctor = async () => {


    let res = await axios.post(`${router}Citas/comboDoctores`);
    res = res.data;
    let cmbCiudad = document.getElementById("idDoctor");



    cmbCiudad.innerHTML = `<option value="">--Seleccione un Doctor--</option>`;

    res.forEach(res => {




        cmbCiudad.innerHTML += `<option value="${res.idDoctor}">${res.nombres + " " + res.apellidos}</option>`;

    });



}


const comboSeguros = async () => {


    let res = await axios.post(`${router}Citas/comboSeguros`);
    res = res.data;
    let cmbCiudad = document.getElementById("idSeguro");



    cmbCiudad.innerHTML = `<option value="">--Seleccione una Opcion--</option>`;

    res.forEach(res => {




        cmbCiudad.innerHTML += `<option value="${res.idSeguro}">${res.institucion + " " + res.tipo}</option>`;

    });



}










comboDoctor();
comboPaciente();
comboSeguros();
listar();
