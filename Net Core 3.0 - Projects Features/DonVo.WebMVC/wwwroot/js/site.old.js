﻿$('input[type=checkbox]').change(function () {

    if(!this.classList.contains("dinamic"))
    {
        return;
    }

    if ($('.' + this.id)[0].classList.contains("invisible"))
    {
        $('.' + this.id)[0].classList.remove("invisible");
    } 
    else 
    {
        $('.' + this.id)[0].classList.add("invisible");
    }
});

$('input[name=TypeAccommodation]').change( function() 
{ 
    if($('fieldset.appartment:not(.form-group.appartment.invisible)')[0] !== undefined) 
    {
        $('fieldset.appartment:not(.form-group.appartment.invisible)')[0].classList.add('invisible');
    }

    $('.' + this.id)[0].classList.remove('invisible');
});

if ($('input[name=TypeAccommodation]:checked').val())
{
    var checkedRadio = $('input[name=TypeAccommodation]:checked').val();
    $('.' + checkedRadio)[0].classList.remove('invisible');
}

$('input[type="checkbox"]:checked').each(function ()
{
    if (window.location.pathname.search('/PositionEmployees/Update/'))
    {
        var checkedChekbox = $(this).attr('id');
        $('.' + checkedChekbox)[0].classList.remove('invisible');
    }
});

$('#editable-select-LevelEducation').editableSelect();

$('#editable-select-Study').editableSelect();

$('#editable-select-ScientifickDegree').editableSelect();

$('#editable-select-ScientifickTitle').editableSelect();

$(function ()
{
    $("#mobile-phone").mask("+38(099)999-99-99");
    $("#Mechnikov-code").mask("9-999999-999999");
    $("#ident-code").mask("9999999999");
    $("#year").mask("9999");
});

//------------------------------------------------------------------------------------------------------------------------------------------

$(document).ready(function ()
{
    if (window.location.pathname === "/Employee/Create")
    {
        var idSubordinateSubdivision = { id: $("#selectMain").val() };
        $.post("/Employee/GetSubordinateSubdivision", idSubordinateSubdivision, getSubordinateSubdivision);
    }
    if (window.location.pathname === "/Search/Index")
    {
        document.getElementById("dormitory").checked = true;
        document.getElementById("idDepartmental").disabled = true;
        document.getElementById("idDormitory").disabled = false;

        document.getElementById("employeeBirthDate").checked = true;
        document.getElementById("employeeHobby").checked = true;
    }
});

$("#selectMain").change(function ()
{
    var idSubordinateSubdivision = { id: $(this).val() };
    $.post("/Employee/GetSubordinateSubdivision", idSubordinateSubdivision, getSubordinateSubdivision);
});

function getSubordinateSubdivision(subordinateSubdivision)
{
    document.getElementById("subordinateSubdivision").innerHTML = " ";
    if (subordinateSubdivision !== null)
    {
        $("#subordinateSubdivision").append("<option value value = null >Select a department</option>");
        $.each(subordinateSubdivision, function ()
        {
            $("#subordinateSubdivision").append("<option value=" + this.hashIdMain + ">" + this.name + "</option>");
        });
    }
    else
    {
        $("#subordinateSubdivision").append("<option value value = null >There are no chairs</option>");
    }
}

//------------------------------------------------------------------------------------------------------------------------------------------

$('input[value=dormitory]').change(function ()
{
    document.getElementById('idDepartmental').disabled = true;
    document.getElementById('idDormitory').disabled = false;
});

$('input[value=departmental]').change(function ()
{
    document.getElementById('idDormitory').disabled = true;
    document.getElementById('idDepartmental').disabled = false;
});

$('input[value=from-university]').change(function ()
{
    document.getElementById('idDormitory').disabled = true;
    document.getElementById('idDepartmental').disabled = true;
});

//------------------------------------------------------------------------------------------------------------------------------------------

$(function () {
    $('.modal').modal('show');
});