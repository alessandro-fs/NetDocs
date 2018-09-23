//---14-12-2015
//FUNÇÃO QUE INSERE O ID E O NOME PARA DELETAR UM REGISTRO 
function SetDeleteActionController(Id, Nome, Controller) {
    try {
        document.getElementById('NomeRegistroExcluir').innerHTML = Nome;
        document.forms["formDelete"].action = "/" + Controller + "/Delete/" + Id;
    } catch (e) { alert(e); }
}