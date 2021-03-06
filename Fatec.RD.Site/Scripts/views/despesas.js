﻿/*********** Declarando variáveis para facilitar ***************/
var api = "";
var formulario = $("#form-despesas");
var body = $("#modal-despesa");
var titleModal = $("#modal-title");
var botaoSalvar = $("#salvar-despesa");
var tabela = $("#tabela-despesas");

var tipoDespesa = $("#tipoDespesa");
var data = $("#data");
var tipoPagamento = $("#tipoPagamento");
var valor = $("#valor");
var comentario = $("#comentario");
/***************************************************************/

listarTabela()

/************* Utilizando componentes em campos ****************/
$(".selecpicker").selectpicker();
data.datepicker({
    language: 'pt-Br',
    todayBtn: "linked",
    clearBtn: true,
    todayHighlight: true
});
data.mask('00/00/0000');
valor.mask("000.000,00", { reverse: true });
/***************************************************************/

/************* Funções disparadas nos cliques ******************/
function novaDespesa() {
    formValidation();
    titleModal.html("Adicionar Desepsa");
    body.modal('show');
}

function fechar() {
    formulario.validate().destroy();
}
/***************************************************************/

/************** Funções com requisições para a API *************/
function inserir(despesa) {
    $.ajax({
        type: 'POST',
        url: api,
        data: despesa,
        success: function () {
            alert("Inserido com sucesso!");
        },
        error: function (error) {
            console.log(error);
        }
    })
}

function alterar(id, despesa) {
    $.ajax({
        type: 'PUT',
        url: api + '/' + id,
        data: despesa,
        success: function () {
            alert("Alterado com sucesso!");
        },
        error: function (error) {
            console.log(error);
        }
    })
}

function excluir(id) {
    $.ajax({
        type: 'DELETE',
        url: api + '/' + id,
        success: function () {
            alert("Deletado com sucesso!");
        },
        error: function (error) {
            console.log(error);
        }
    })
}

function selecionarPorId(id) {
    $.ajax({
        type: 'GET',
        url: api + '/' + id,
        success: function (despesa) {
            console.log(despesa);
        },
        error: function (error) {
            console.log(error);
        }
    })
}
/***************************************************************/

/*********************** Funções internas **********************/

function listarTabela() {
    var t = tabela.mDatatable({
        translate: {
            records: {
                noRecords: "Nenhum resultado encontrado.",
                processing: "Processando..."
            },
            toolbar: {
                pagination: {
                    items: {
                        default: {
                            first: "Primeira",
                            prev: "Anterior",
                            next: "Próxima",
                            last: "Última",
                            more: "Mais",
                            input: "Número da página",
                            select: "Selecionar tamanho da página"
                        },
                        info: 'Exibindo' + ' {{start}} - {{end}} ' + 'de' + ' {{total}} ' + 'resultados'
                    },
                }
            }
        },
        data: {
            type: "remote",
            source: {
                read: {
                    method: "GET",
                    url: "inc/api/datatables/demos/default.php",
                    map: function (t) {
                        var e = t;
                        return void 0 !== t.data && (e = t.data), e
                    }
                }
            },
            pageSize: 10,
            serverPaging: !0,
            serverFiltering: !0,
            serverSorting: !0
        },
        layout: {
            theme: "default",
            class: "",
            scroll: !1,
            footer: !1
        },
        sortable: !0,
        pagination: !0,
        toolbar: {
            items: {
                pagination: {
                    pageSizeSelect: [10, 20, 30, 50, 100]
                }
            }
        },
        search: {
            input: $("#generalSearch")
        },
        columns: [
            {
                field: "Data",
                title: "Data",
                width: 40,
                selector: !1,
                textAlign: "center",
                sortable: "asc",
                type: "date",
                format: "DD/MM/YYYY"
            },
            {
                field: "TipoDespesa",
                title: "Tipo Despesa",
                filterable: !1,
            },
            {
                field: "Valor",
                title: "Valor",
            },
            {
                field: "TipoPagamento",
                title: "Tipo Pagamento"
            },
            {
                field: "Comentario",
                title: "Comentário"
            },
            //{
            //    field: "Status",
            //    title: "Status",
            //    template: function (t) {
            //        var e = {
            //            1: { title: "Pending", class: "m-badge--brand" },
            //            2: { title: "Delivered", class: " m-badge--metal" },
            //            3: { title: "Canceled", class: " m-badge--primary" },
            //            4: { title: "Success", class: " m-badge--success" },
            //            5: { title: "Info", class: " m-badge--info" },
            //            6: { title: "Danger", class: " m-badge--danger" },
            //            7: { title: "Warning", class: " m-badge--warning" }
            //        };
            //        return '<span class="m-badge ' + e[t.Status].class + ' m-badge--wide">' + e[t.Status].title + "</span>"
            //    }
            //},
            {
                field: "Acoes",
                title: "Ações",
                sortable: false,
                overflow: "visible",
                template: function (t, e, a) {
                    return '\
                            <div class="dropdown ' + (a.getPageSize() - e <= 4 ? "dropup" : "") + '">\
                                <a href="#" class="btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="dropdown">\
                                    <i class="la la-ellipsis-h"></i>\
                                </a>\
                                <div class="dropdown-menu dropdown-menu-right">\
                                    <a class="dropdown-item" href="#">\
                                        <i class="la la-edit"></i> Edit Details\
                                    </a>\
                                    <a class="dropdown-item" href="#">\
                                        <i class="la la-leaf"></i> Update Status\
                                    </a>\
                                    <a class="dropdown-item" href="#">\
                                        <i class="la la-print"></i> Generate Report\
                                    </a>\
                                </div>\
                                <a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" title="Edit details">\
                                    <i class="la la-edit"></i>\
                                </a>\
                                <a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Delete">\
                                    <i class="la la-trash"></i>\
                                </a>\
                            </div>';
                }
            }]
    }),
    e = t.getDataSourceQuery();
    $("#m_form_status").on("change", function () {
        var e = t.getDataSourceQuery();
        e.Status = $(this).val().toLowerCase(),
        t.setDataSourceQuery(e),
        t.load()
    }).val(void 0 !== e.Status ? e.Status : ""),
    $("#m_form_type").on("change", function () {
        var e = t.getDataSourceQuery();
        e.Type = $(this).val().toLowerCase(),
        t.setDataSourceQuery(e),
        t.load()
    }).val(void 0 !== e.Type ? e.Type : ""),
    $("#m_form_status, #m_form_type").selectpicker();
}

function salvarDespesa() {
    var id = $(this).attr("data-id");
    var despesa = formulario.serialize();

    if (id != undefined)
        alterar(id, depesa);
    else
        inserir(despesa);
}

function preencherCampos(despesa) {
    tipoDespesa.val();
    data.val();
    tipoPagamento.val();
    valor.val();
    comentario.val();
}

function limparCampos() {
    formulario.each(function () {
        this.reset();
    });

    botaoSalvar.removeAttr('data-id');
}

/***************************************************************/

/************************* Validações **************************/
function formValidation() {
    formulario.validate({
        errorClass: "errorClass",
        rules: {
            tipoDespesa: { required: true },
            data: { required: true },
            tipoPagamento: { required: true },
            valor: { required: true }
        },
        messages: {
            tipoDespesa: { required: "Campo obrigatório." },
            data: { required: "Campo obrigatório." },
            tipoPagamento: { required: "Campo obrigatório." },
            valor: { required: "Campo obrigatório." /*, minlength: "Campo deve possuir no mínimo {0} caracteres", maxlength: "Campo deve possuir no máximo {0} caracteres"*/ }
        },
        submitHandler: function (form) {
            salvarDespesa();
        }
    });
}