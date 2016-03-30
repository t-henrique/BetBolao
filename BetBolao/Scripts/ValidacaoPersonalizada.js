function habilitarBotao(chk) {
    var coffee = document.forms[0];
    //var chk = document.getElementById("checkboxList");
    var btn = document.getElementById("btnDeletar");
    if (chk.checked) {
        btn.disabled = "";
    }
    else {
        btn.disabled = "Disabled";
    }

        for (var i = 0; i < chk.length; i++) {

            if (chk[i].checked === true) {
                btn.disabled = "";
                return btn.disabled = "";
            } else {
                btn.disabled = "Disabled";
            }
        }
    
};

var inputs = $('input');
inputs.on('keyup', verificarInputs);

function verificarInputs() {
    var preenchidos = true;  // assumir que estão preenchidos
    inputs.each(function () {
        // verificar um a um e passar a false se algum falhar
        // no lugar do if pode-se usar alguma função de validação, regex ou outros
        if (!this.value) {
            preenchidos = false;
            // parar o loop, evitando que mais inputs sejam verificados sem necessidade
            return false;
        }
    });
    // Habilite, ou não, o <button>, dependendo da variável:
    $('button').prop('disabled', !preenchidos); // 
}
//$ habilitarBotaoFinalizar(txtLancarPlacar) {
//    var coffee = document.forms[0];
//    //var chk = document.getElementById("checkboxList");
//    var btn = document.getElementById("btnFinalizarJogos");
//    if (txtLancarPlacar.value === "") {
//        btn.disabled = "";
//    }
//    else {
//        btn.disabled = "Disabled";
//    }


//};


// FUNCTION DE EXEMPLO PARA PASSAR CHEKLIST PARA O CONTROLER VIA JSON
//function preencheCheckList(chk) {
//    var chkbox = new Array();
//    for (var i = 0; i < chk.length; i++) {
//        if (chk[i].checked) {
//            //chkbox[i] = chk[i].value;
//            chkbox.push(chk[i].value);
//            alert(chkbox);
//        } else {
//            //chkbox[i] = "";
//            chkbox.push("0");
//            alert(chkbox);
//        }
//    }
//    alert(chkbox);
//    $.ajax({
//        type: "POST",
//        cache: false,
//        url: "/Competicao/ExcluirJogosCompeticao",
//        dataType: "JSON",
//        traditional: true,
        
//            data: { chkbox: chkbox },
//            success: function() {
//                alert("ok");
//            },
//            error: function (jqXHR, exception) {
//                alert(jqXHR.messages);
//                alert(exception.ExceptionInformation);
//            }
//        })};

//$(document).ready(function() {
    
//    $(".chkclass").click(function () {

//        var getchkid = $(this).attr("id");
//        var isChecked = $("#" + getchkid).is(":checked");
//            if ($("#"+ getchkid).is(":checked") == true) {
//                $("#td").css("color", "white");
//                $("#td").css("background-color", "blue");
//            } else {
//                $("#td").css("color", "black");
//                $("#td").css("background-color", "white");
//            }
//    });
//})