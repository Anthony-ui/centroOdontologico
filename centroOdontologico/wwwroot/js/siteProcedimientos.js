const btnGuardar = document.getElementById("guardar");
const btnSalir = document.getElementById("salir");
const btnNuevo = document.getElementById("nuevo");
const deciml = document.querySelector(".decimal");
const router = new Ruta().pack;
const cuerpoTabla = document.querySelector("tbody");
const modal = new bootstrap.Modal(document.getElementById("modal"),

    {
        keyboard: false,
        backdrop: "static"
    });

var idProcedimiento = 0;
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


deciml.addEventListener("keyup", res => {
    decimal(deciml.value)
});

const guardar = async () => {


    let costo = document.getElementById("costo");
    let parse = costo.value.replace(".", ",");
    costo.value = parse;







    const val = await validaciones();

    if (val == "ok") {
        tata.error('', 'Todos los campos son requeridos');

        return;
    }
    const form = document.getElementById('formulario');
    const formulario = new FormData(form);

    formulario.append("idProcedimiento", idProcedimiento);

    const res = await axios.post(`${router}Procedimientos/guardar`, formulario);

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
    const res = await axios.post(`${router}Procedimientos/listar`);

    if (res.data.length == 0) {

        tabla = `<tr>


                   <td colspan='5' class='text-center'>

                       No Existen Registros
                      
                   </td>

        
                </tr> `;
    }



    res.data.forEach(res => {
        cont = cont + 1;

        tabla += `<tr>

                  <td class='text-center'>${cont}</td>

                  <td class='text-center'>${res.nombre}</td>

                  <td class='text-center'>${res.detalle}</td>

                  <td class='text-center'>${res.costo}</td>


                 <td class='text-center'>

                <div class="dropdown">
                <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
                Opciones
                </button>
                <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                <li><a class="dropdown-item" onclick="editar(${res.idProcedimiento})">Editar</a></li>
                <li><a class="dropdown-item" onclick="eliminar(${res.idProcedimiento})">Eliminar</a></li>
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

    formulario.append("idProcedimiento", id);
    let res = await axios.post(`${router}Procedimientos/cargar`, formulario);
    res = res.data;
    idProcedimiento = res.idProcedimiento;
    patchValues(res);
    modal.show();



}



const patchValues = (obj) => {

    Object.keys(obj).forEach((Objeckey) => {

        let element = document.getElementById(`${Objeckey}`);
        if (!!element) element.value = obj[`${Objeckey}`];
    })

    let costo = document.getElementById("costo");
    let parse = costo.value.replace(".", ",");
    costo.value = parse;
}



const eliminar = async (id) => {



    if (confirm("El registro se eliminara de manera permanente")) {


        const formulario = new FormData();
        formulario.append("idProcedimiento", id);
        const url = `${router}Procedimientos/eliminar`
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





listar();
