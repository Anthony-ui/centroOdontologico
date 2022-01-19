
const router = new Ruta().pack;
var detalleObj = [];
var btnAgregar = document.getElementById("agregar");
const detalle = document.getElementById("detalle");
const idCita = document.getElementById("idCita");
var acum = 0;
const btnGuardar = document.getElementById("guardar");
var btnCancelar = document.getElementById("cancelar");



btnGuardar.addEventListener("click", res => {
    guardar();
});






btnAgregar.addEventListener("click", () => {




    acum = 0;

   

    const precedimiento = document.getElementById(`idProcedimiento`);
    if (precedimiento.value == "") {

        alert("Seleccione un procedimiento");

        return;
    }
    if (detalleObj.filter(x => x.idProcedimiento == precedimiento.value).length > 0) {

        tata.error("", "Este procedimiento ya se encuentra Agregado");

        return;
    }
    detalleObj.push(
        {

            "idProcedimiento": precedimiento.value,
            "carrera": precedimiento.options[precedimiento.selectedIndex].text,
            "valor": precedimiento.options[precedimiento.selectedIndex].dataset.costo,
            "idCita":idCita.innerText


        }

    
      
    );

    console.log(detalleObj);




    llenarListaCarrera(detalleObj);
    document.querySelectorAll(".costo").forEach(res => {

      acum= parseFloat(acum) +  parseFloat(res.innerText);

    });


    document.getElementById("total").innerHTML = "$ " + acum;  

 
});


const llenarListaCarrera = (obj) => {


    
    let html = "";
    detalle.innerHTML = "";
    obj.forEach(res => {
        html += `
                    <tr data-iDetalle='${res.idProcedimiento}'>
                        <td>${res.carrera}</td>
                        <td class="costo">${res.valor}</td>
                        <td><button type='button' class='btn-sm btn btn-outline-primary'
                            style='border:none !important' onclick='remover(${res.idProcedimiento})'>
                            Eliminar
                            </button>
                        </td>
                    </tr>
                `;
    });
    detalle.innerHTML = html;
}


const remover = (id) => {


    acum = 0;

    let index = detalleObj.indexOf(detalleObj.filter(x => x.idProcedimiento == id));
    detalleObj.splice(index, 1);
    detalle.querySelector(`[data-iDetalle='${id}']`).remove();

    if (detalleObj.length == 0) {
         
        detalle.innerHTML = "<tr><td colspan='2' class='text-center'>Ninguna carrera seleccionada</td></tr>";
    }


    document.querySelectorAll(".costo").forEach(res => {


        acum = parseFloat(acum) + parseFloat(res.innerText);

    });


    document.getElementById("total").innerHTML = "$ " + acum;

}

const comboProcedimiento = async () => {

    try {


        let res = await axios.post(`${router}Calendario/listar`);
        res = res.data;
        let cmbProcedimiento = document.getElementById("idProcedimiento");




        res.forEach(res => {


            cmbProcedimiento.innerHTML += `<option value="${res.idProcedimiento}" 
        <tr data-costo='${res.costo}'>${res.nombre}</option>`;

        });




    } catch (e) {

        tata.error('', 'Se ha producido un error');

    }
    



}



const guardar = async () => {


    try {


        const val = await validaciones();

        if (val == "ok") {
            tata.error('', 'Todos los campos son requeridos');

            return;
        }



        if (confirm("Está Seguro que desea generar la Cita")) {



            let obj = new FormData();

            obj = detalleObj;

            const res = await axios.post(`${router}Calendario/guardar`, obj);


            if (res.data == "ok") {

                tata.success('', 'Cita generada correctamente');

                vaciar();
                modal.hide();
                setTimeout(() => {

                    window.location.reload();

                }, 3000);
            }


        }



    } catch (e) {

        tata.error('', 'Error al generar la cita');

    }



}

const cancelar = async (id) => {

    try {

        if (confirm("Está Seguro que desea cancelar la Cita")) {


            const res = await axios.post(`${router}Calendario/cancelar`, null, {
                params: {

                    id,

                }
            });


            if (res.data == "ok") {

                tata.success('', 'Se ha Cancelado la Cita Correctamente');

                vaciar();
                modal.hide();
                setTimeout(() => {

                    window.location.reload();

                }, 3000);
            }
        }

    } catch (e) {
        tata.error('', 'Error al cancelar la cita');

    }

}

comboProcedimiento();