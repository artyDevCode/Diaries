
$.ajaxSetup({ cache: false });

$(function () {

    var selectedDiaryName;
    var selectedListId;
    var pathArray;
    var pathURL;
    var returnval = false;
    var icount;
    //var csday;
    //var csmonth;
    //var csyear;
    //var ceday;
    //var cemonth;
    //var ceyear;
    //var nsday;
    //var nsmonth;
    //var nsyear;
    //var neday;
    //var nemonth;
    //var neyear;
    //var maxdate1;
    //var parsed_date;

    $(document).init( function () {
        $(".box").hide();
        $(".red").show();

        $('#loading-image').hide();

        getLisingInfoTable("01/01/1970","01/01/1970");
        $("#ListingInformationSection").fadeIn();

        getLisingRulesTable();
        $("#ListingRulesSection").show();
        $("#ListingRulesSection").fadeIn();

        //parsed_date = new Date(2014 - 1, 12, 31);
        //maxdate1 = new Date();
        //maxdate1.setDate(parsed_date.getDate());

        //csday = document.getElementById("CurrentStartDate").val().split('-')[0];
        //csmonth = document.getElementById("CurrentStartDate").val().split('-')[1];
        //csyear = document.getElementById("CurrentStartDate").val().split('-')[2];
        //ceday = document.getElementById("CurrentEndDate").val().split('-')[0];
        //cemonth = document.getElementById("CurrentEndDate").val().split('-')[1];
        //ceyear = document.getElementById("CurrentEndDate").val().split('-')[2];

        //document.getElementById("CurrentStartDate").value = "01/12/2014";
        //document.getElementById("CurrentEndDate").value = "01/12/2014";
        //document.getElementById("NextStartDate").value = "01/12/2014";
        //document.getElementById("NextEndDate").value = "01/12/2014";
    });

    //var pickerOpts = { 
    //    minDate: new Date(), 
    //    maxDate: "+10" 
    //};   

    //$(".datepickerlistyear").init(function () {
    //    //alert($('#CurrentStartDate').val().split('-')[0]);
    //    csday = $('#CurrentStartDate').val().split('-')[0]
    //    csmonth = $('#CurrentStartDate').val().split('-')[1]
    //    csyear = $('#CurrentStartDate').val().split('-')[2]
    //    ceday = $('#CurrentStartDate').val().split('-')[0]
    //    cemonth = $('#CurrentStartDate').val().split('-')[1]
    //    ceyear = $('#CurrentStartDate').val().split('-')[2]
    //    alert(csday + csmonth + csyear)
    //})

    $('input[type="radio"]').click(function () {
        if ($(this).attr("id") == "RadioCurrentYearFilter") {
            $(".box").hide();
            $(".red").show();
        }
        if ($(this).attr("id") == "RadioNextYearFilter") {
            $(".box").hide();
            $(".green").show();
        }
        //if ($(this).attr("id") == "DisableEntireRange") {
        //}
        //if ($(this).attr("id") == "DontDisableEntireRange") {
        //}
    });

    //$(".datepickercurrentyearList").datepicker({
    //    minDate: new Date($('#CurrentStartYear').val(), $('#CurrentStartMonth').val(), -30, $('#CurrentStartDay').val()),
    //    maxDate: new Date($('#CurrentEndYear').val(), $('#CurrentEndMonth').val(), +2, -$('#CurrentEndDay').val()),
    //    changeMonth: true,
    //    changeYear: true,
    //    //showOtherMonths: true,
    //    //selectOtherMonths: true,
    //    //dateFormat: 'DD, d MM, yy',
    //    altField: '#date_due',
    //    altFormat: 'yy-mm-dd',
    //    firstDay: 1, // rows starts on Monday 11/7/2014
    //    dateFormat: "dd-mm-yy",
    //    timeFormat: "hh:mm tt",
    //})

    //$(".datepickernextyearList").datepicker({
    //    minDate: new Date($('#NextStartYear').val(), $('#NextStartMonth').val(), -30, $('#NextStartDay').val()),
    //    maxDate: new Date($('#NextEndYear').val(), $('#NextEndMonth').val(), +2, -$('#NextEndDay').val()),
    //    changeMonth: true,
    //    changeYear: true,
    //    //showOtherMonths: true,
    //    //selectOtherMonths: true,
    //    //dateFormat: 'DD, d MM, yy',
    //    altField: '#date_due',
    //    altFormat: 'yy-mm-dd',
    //    firstDay: 1, // rows starts on Monday 11/7/2014
    //    dateFormat: "dd-mm-yy",
    //    timeFormat: "hh:mm tt",
    //})

    $(".datepickercurrentyear").datepicker({
        minDate: new Date($('#CurrentStartYear').val(), $('#CurrentStartMonth').val(), -30, $('#CurrentStartDay').val()),
        maxDate: new Date($('#CurrentEndYear').val()  , $('#CurrentEndMonth').val(), +2 , - $('#CurrentEndDay').val()),
        beforeShowDay: $.datepicker.noWeekends,
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        altField: '#date_due',
        altFormat: 'yy-mm-dd',
        firstDay: 1, 
        dateFormat: "dd/mm/yy",
        timeFormat: "hh:mm tt",
        required: true,
        dpDate: true
        //{ minDate: -20, maxDate: "+1M +10D" }
    })

    //$("#samplecode").validate({
    //    rules: {
    //        datepickercurrentyear: { required: true, dpDate: true },
    //        datepickernextyear: { required: true, dpDate: true }
    //    }

    // });


    $(".datepickernextyear").datepicker({
        minDate: new Date($('#NextStartYear').val(), $('#NextStartMonth').val(), -30, $('#NextStartDay').val()),
        maxDate: new Date($('#NextEndYear').val(), $('#NextEndMonth').val(), -1, $('#NextEndDay').val()),
        //minDate: new Date($('#NextStartDate').val().split('-')[2], $('#NextStartDate').val().split('-')[1] - 1, $('#NextStartDate').val().split('-')[0]),
        //maxDate: new Date($('#NextEndDate').val().split('-')[2], $('#NextEndDate').val().split('-')[1] - 1, $('#NextEndDate').val().split('-')[0]), //"+10",
        beforeShowDay: $.datepicker.noWeekends,
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        //showOn: "button",
        //buttonImage: "images/calendar.gif",
        //showOtherMonths: true,
        //selectOtherMonths: true,
        //dateFormat: 'DD, d MM, yy',
        altField: '#date_due',
        altFormat: 'yy-mm-dd',
        firstDay: 1, // rows starts on Monday 11/7/2014
        dateFormat: "dd/mm/yy",
        timeFormat: "hh:mm tt",
        required: true,
        dpDate: true //{ minDate: -20, maxDate: "+1M +10D" }
    })


    function checkdate(input){
        var validformat=/^\d{2}\/\d{2}\/\d{4}$/ //Basic check for format validity
        var returnval=false
        if (!validformat.test(input.value))
            alert("Invalid Date Format. Please correct and submit again.")
        else{ //Detailed check for valid date ranges
            var monthfield=input.value.split("/")[1]
            var dayfield=input.value.split("/")[0]
            var yearfield=input.value.split("/")[2]
            var dayobj = new Date(yearfield, monthfield-1, dayfield)
            if ((dayobj.getMonth()+1!=monthfield)||(dayobj.getDate()!=dayfield)||(dayobj.getFullYear()!=yearfield))
                alert("Invalid Day, Month, or Year range detected. Please correct and submit again.")
            else
                returnval=true
        }
        //if (returnval==false) input.select()
        return returnval
    }

    function CurrentStartDateValid() {

        var validformat = /^\d{2}\/\d{2}\/\d{4}$/ //Basic check for format validity
        dinput = document.getElementById("CurrentFromDate").value
        returnval = false
        if (!validformat.test(document.getElementById("CurrentFromDate").value))
            returnval = false
            //alert("Invalid Start Date Format. Please review and submit again.")
        else { //Detailed check for valid date ranges
            var monthfield = dinput.split("/")[1]
            var dayfield = dinput.split("/")[0]
            var yearfield = dinput.split("/")[2]
            var dayobj = new Date(yearfield, monthfield - 1, dayfield)
            if ((dayobj.getMonth() + 1 != monthfield) || (dayobj.getDate() != dayfield) || (dayobj.getFullYear() != yearfield))
                returnval = false
                //alert("Invalid Start Day, Month, or Year range detected. Please review and submit again.")
            else
                returnval = true
        }
    }

    function CurrentEndDateValid() {
        var validformat = /^\d{2}\/\d{2}\/\d{4}$/ //Basic check for format validity
        dinput = document.getElementById("CurrentToDate").value
        returnval = false
        if (!validformat.test(dinput))
            returnval = false
            //alert("Invalid End Date Format. Please review and submit again.")
        else { //Detailed check for valid date ranges
            var monthfield = dinput.split("/")[1]
            var dayfield = dinput.split("/")[0]
            var yearfield = dinput.split("/")[2]
            var dayobj = new Date(yearfield, monthfield - 1, dayfield)
            if ((dayobj.getMonth() + 1 != monthfield) || (dayobj.getDate() != dayfield) || (dayobj.getFullYear() != yearfield))
                returnval = false
                //alert("Invalid End Day, Month, or Year range detected. Please review and submit again.")
            else
                returnval = true

        }
    }


    $(document).on('click', '#SearchCList', function () {


        var dinput = document.getElementById("CurrentFromDate").value

        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/ListType/GetCurrentListingInformation";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/ListType/GetCurrentListingInformation";
        }

        if (document.getElementById("CurrentFromDate").value == "") {
            alert("Enter the current search from date");
            $("#CurrentFromDate").focus();
        }
        else if (document.getElementById("CurrentToDate").value == "") {
            alert("Enter the current search end date");
            $("#CurrentToDate").focus();
        }
        else if (document.getElementById("CurrentFromDate").value != "")
            CurrentStartDateValid();
            if (returnval == false)  
            {
                alert("Invalid Start Date Format. Please review and submit again.");
                $("#CurrentFromDate").focus();
            }

        else if (document.getElementById("NextFromDate").value != "")
                CurrentEndDateValid();
        if (returnval == false) {
            alert("Invalid End Date Format. Please review and submit again.");
            $("#CurrentToDate").focus();
        }

        //else if (document.getElementById("CurrentFromDate").value != "") {
        //    var validformat = /^\d{1}\/\d{2}\/\d{4}$/ //Basic check for format validity
        //    var returnval = false
        //    if (!validformat.test(dinput))
        //        alert("Invalid Date Format. Please correct and submit again.")
        //    else { //Detailed check for valid date ranges
        //        var monthfield = dinput.split("/")[0]
        //        var dayfield = dinput.split("/")[1]
        //        var yearfield = dinput.split("/")[2]
        //        var dayobj = new Date(yearfield, monthfield - 1, dayfield)
        //        if ((dayobj.getMonth() + 1 != monthfield) || (dayobj.getDate() != dayfield) || (dayobj.getFullYear() != yearfield))
        //            alert("Invalid Day, Month, or Year range detected. Please correct and submit again.")
        //        else
        //            returnval = true
        //    }

        //    //alert("Enter the current search from date");
        //    //$("#CurrentFromDate").focus();
        //}
        //else if (document.getElementById("D_L_ShortName").value == "") {
        //    alert("List short name must be entered");
        //    $("#D_L_ShortName").focus();
        //}
        //else if ($("#D_L_ShortName").val().length > 8) {
        //    alert("List short name must be 8 characters");
        //    $("#D_L_ShortName").focus();
            //}

        else 
        {
            $('#loading-image').show();

            $.ajax({
                url: pathURL,
                type: 'Get',
                data: { listId: $('#hid_DLid').val(), diaryId: $('#hid_Did').val(), Start: $('#CurrentFromDate').val(), End: $('#CurrentToDate').val() },
                success: function (data) {
                    $("#ListingInformationDetailTable").empty().append(data);
                    getLisingRulesTable($('#CurrentFromDate').val(), $('#CurrentToDate').val());
                    $('#loading-image').hide();
                },
                error: function (xhr, textStatus, errorThrown) {
                    //alert(xhr.responseText);
                    alert("Error retrieving listing information, please refresh view.");
                    $('#loading-image').hide();
                }
            });
        }

        

    });


    function getLisingInfoTable(start, end) {
        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/ListType/GetCurrentListingInformation";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/ListType/GetCurrentListingInformation";
        }

        $.ajax({
            // Get List Types PartialView
            url: pathURL,
            type: 'Get',
            data: { listId: $('#hid_DLid').val(), diaryId: $('#hid_Did').val(), Start: start, End: end },
            //data: { listId: $('#hid_DLid').val(), diaryId: $('#hid_Did').val(), Start: "01/01/1970", End: "01/01/1970" },
            //data: { listId: $('#hid_DLid').val(), diaryId: $('#hid_Did').val(), Start: $('#CurrentFromDate').val(), End: $('#CurrentToDate').val() },
            success: function (data) {
                $("#ListingInformationDetailTable").empty().append(data);
                //$('#loading-image').hide();
            },
            error: function (xhr, textStatus, errorThrown) {
                //alert(xhr.responseText);
                alert("There has been an error loading list types, please retry selection.");
            }
        });
    }

    function getLisingRulesTable(start, end ) {
        //$('#loading-image').show();

        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/ListType/GetCurrentListingRules";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/ListType/GetCurrentListingRules";
        }

        start = typeof start !== 'undefined' ? start : "01/01/1970";
        end = typeof end !== 'undefined' ? end : "01/01/1970";

        $.ajax({
            // Get List Types PartialView
            url: pathURL,
            type: 'Get',
            data: { listId: $('#hid_DLid').val(), diaryId: $('#hid_Did').val(), Start: start, End: end },
            //data: { listId: $('#hid_DLid').val(), diaryId: $('#hid_Did').val(), Start: "01/01/1970", End: "01/01/1970" },
            //data: { listId: $('#hid_DLid').val(), diaryId: $('#hid_Did').val(), Start: $('#CurrentFromDate').val(), End: $('#CurrentToDate').val() },
            success: function (data) {
                $("#ListingRuleDetailTable").empty().append(data);
                $(".datepickercustomList").datepicker({
                    minDate: new Date($('#CurrentStartYear').val(), $('#CurrentStartMonth').val(), -30, $('#CurrentStartDay').val()),
                    maxDate: new Date($('#NextEndYear').val(), $('#NextEndMonth').val(), +2, -$('#NextEndDay').val()),
                    changeMonth: true,
                    changeYear: true,
                    beforeShowDay: $.datepicker.noWeekends,
                    altField: '#date_due',
                    altFormat: 'yy-mm-dd',
                    firstDay: 1, // rows starts on Monday 11/7/2014
                    dateFormat: "dd/mm/yy",
                    timeFormat: "hh:mm tt",
                    Default: ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"]
                })
                $("#V_Limit").focus();
               
            },
            error: function (xhr, textStatus, errorThrown) {
                //alert(xhr.responseText);
                alert("There has been an error loading list rules, please retry selection.");
            }
        });

        //$('#loading-image').hide();

    } 
    var counter = 0;

    $(document).on('change', '#datepickercustomList', function () {
        //alert($(this).val());
        //day = $(this).val().getDay()
        //document.getElementById('ListTypeMngDetail').innerHTML[i++] = $(this).val();// + "<a href='" + Remove + "' >" + Remove + "</a>";

        //from = $(this).val().split("/");
        //d = new Date(from[2], from[1] - 1, from[0]);
        //var weekday = new Array(7);

        var date1 = $(".datepickercustomList").datepicker('getDate');
        date = $.datepicker.formatDate("dd/mm/yy DD", date1);
        //weekday[0] = "Monday";
        //weekday[1] = "Tuesday";
        //weekday[2] = "Wednesday";
        //weekday[3] = "Thursday";
        //weekday[4] = "Friday";
        //weekday[5] = "Saturday";
        //weekday[6] = "Sunday";

        //var n = weekday[d.getDay()];
        //alert(d);
        addRow('dataTable', date, counter);
        counter++;
    });

    function deleteRow(rowcount) {
        try {
            var table = document.getElementById('dataTable');
           // var rowCount = table.rows.length;
          //  table.deleteRow(rowcount);
        
            $("#" + rowcount).remove();
            //for (var i = 0; i < rowCount; i++) {
            //    var row = table.rows[i];
            //    //var chkbox = row.cells[0].childNodes[0];
            //    //if (null != chkbox && true == chkbox.checked) {
            //    //if (rowcount == row.cells[i].childNodes[0]) {
            //    if (rowcount == row.cells[i]innerHTML) {
            //            table.deleteRow(i);
            //        rowCount--;
            //        i--;
            //        break;
            //    }
            //}
        } catch (e) {
            alert(e);
        }
    }


    function addRow(tableID, date, count) {

        var table = document.getElementById(tableID);

        var rowCount = table.rows.length;
        var row = table.insertRow(rowCount);
        row.id = count;
        var cell1 = row.insertCell(0);
        var element1 = document.createElement("text");
        element1.type = "text";
        element1.name = "textday";
        element1.innerHTML = date;
        element1.id = count;
        cell1.appendChild(element1);

        //var cell2 = row.insertCell(1);
        //cell2.innerHTML = rowCount;

        //var cell2 = row.insertCell(1);
        //var element1 = document.createElement("text");
        //element1.type = "text";
        //element1.name = "textdate";
        //element1.innerHTML = dday;
        //cell1.appendChild(element1);

        var cell3 = row.insertCell(1);
        var element2 = document.createElement("a");
        element2.type = "a";
        element2.name = "txtlik";
        //element2.setAttribute('href', "Remove");
        //element2.setAttribute('href', 'javascript:deleteRow(rowcount)');
        element2.innerHTML = "Remove";

        //element2.setAttribute('onclick', 'deleteRow(rowcount);'); // for FF
        element2.onclick = function () { deleteRow(count); }; // for IE
        //element2.onclick = (function (id) { return function () { deleteRow(rowcount); } })(dval);
        cell3.appendChild(element2);


    }

    $(document).on('click', '#SearchNList', function () {

        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/ListType/GetCurrentListingInformation";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/ListType/GetCurrentListingInformation";
        }


        if (document.getElementById("NextFromDate").value == "") {
            alert("Enter the next search from date");
            $("#NextFromDate").focus();
        }
        else if (document.getElementById("NextToDate").value == "") {
            alert("Enter the bext search end date");
            $("#NextEndDate").focus();
        }
        //else if (document.getElementById("D_L_ShortName").value == "") {
        //    alert("List short name must be entered");
        //    $("#D_L_ShortName").focus();
        //}
        //else if ($("#D_L_ShortName").val().length > 8) {
        //    alert("List short name must be 8 characters");
        //    $("#D_L_ShortName").focus();
        //}
        else {
            $('#loading-image').show();
            //$('#loading-image').show();
            //$('#loading-image').show();
            //$('#loading-image').show();

            $.ajax({
            url: pathURL,
            type: 'Get',
            data: { listId: $('#hid_DLid').val(), diaryId: $('#hid_Did').val(), Start: $('#NextFromDate').val(), End: $('#NextToDate').val() },
            success: function (data) {
                $("#ListingInformationDetailTable").empty().append(data);
                getLisingRulesTable($('#NextFromDate').val(), $('#NextToDate').val());
                $("#V_Limit").focus();
                $('#loading-image').hide();
            },
            error: function (xhr, textStatus, errorThrown) {
                //alert(xhr.responseText);
                //error: function (data) {
                alert("Error retrieving listing information, please refresh view.");
                $('#loading-image').hide();
            }
        });
        }
    });

    $(document).on('click', '#CancelListRules', function () {
        var r = confirm("Do you wish to cancel? No data will be saved. Please confirm");
        if (r == true) {
            location.reload(true);
        }
    });

    $(document).on('click', '#CancelDisableRules', function () {
        var r = confirm("Do you wish to cancel? No data will be saved. Please confirm");
        if (r == true) {
            location.reload(true);
        }
    });

    $(document).on('click', '#SaveListRules', function () {


        var r = confirm("This will overwrite listing rules? Please confirm");
        if (r == true) {

            var start, end

            if ($('#RadioCurrentYearFilter').is(':checked'))
            {
                start = $('#CurrentFromDate').val();
                end = $('#CurrentToDate').val();
            }
            else {
                start = $('#NextFromDate').val();
                end = $('#NextToDate').val();
            }

            pathArray = window.location.href.split('/');
            if (window.location.hostname.indexOf("localhost") >= 0) {
                pathURL = "/ListType/SaveListRules";
            }
            else {
                pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/ListType/SaveListRules";
            }

            if (document.getElementById("V_Limit").value == "") {
                alert("Limit name must be entered");
                $("#V_Limit").focus();
            }
            else
            {
                $('#loading-image').show();
                delay(100);
                $.ajax({
                    url: pathURL,
                    type: 'Post',
                    data: { diaryId: $('#hid_Did').val(), listId: $('#hid_DLid').val(), Limit: $('#V_Limit').val(), Reason: $('#V_Notes').val(), startDate: start, endDate: end, mondaySelected: $('#MondaySelected').is(':checked'), tuesdaySelected: $('#TuesdaySelected').is(':checked'), wednesdaySelected: $('#WednesdaySelected').is(':checked'), thursdaySelected: $('#ThursdaySelected').is(':checked'), fridaySelected: $('#FridaySelected').is(':checked') },
                    success: function (data) {
                        if (document.getElementById("hid_DLid").value == "") {
                            alert("Save Successful")
                            location.reload(true);
                        }
                        else {
                            //$("#ListingInformationDetailTable").empty().append(data);
                            getLisingInfoTable(start, end)
                            $('#loading-image').hide();
                            alert("Save Successful");
                        }

                    },
                    error: function (data) {
                        $('#loading-image').hide();
                        alert("Unable to save list rules, Please review.");
                    }
                });
            }

        } 
        else { x = "You pressed Cancel!";
        }
        //$('#loading-image').hide();

    });

    function delay(time) {
        var d1 = new Date();
        var d2 = new Date();
        while (d2.valueOf() < d1.valueOf() + time) {
            d2 = new Date();
        }
    }
    $(document).on('click', '#SaveDisableRules', function () {

        var r = confirm("This will DISABLE listing rules and cannot be undone. Please confirm");
        if (r == true) {

            var start, end, disablerange

            if ($('#RadioCurrentYearFilter').is(':checked')) {
                start = $('#CurrentFromDate').val();
                end = $('#CurrentToDate').val();
            }
            else {
                start = $('#NextFromDate').val();
                end = $('#NextToDate').val();
            }

            if ($('#DisableEntireRange').is(':checked'))
            {
                disablerange = "true";
            }
            else
            {
                disablerange = "false";
            }

            pathArray = window.location.href.split('/');
            if (window.location.hostname.indexOf("localhost") >= 0) {
                pathURL = "/ListType/DisableListRules";
            }
            else {
                pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/ListType/DisableListRules";
            }

            //if (document.getElementById("V_Notes").value == "") {
            //    alert("Limit name must be entered");
            //    $("#V_Limit").focus();
            //}
            //else {
                $('#loading-image').show();
                delay(100);

                $.ajax({
                    url: pathURL,
                    type: 'Post',
                    data: { diaryId: $('#hid_Did').val(), listId: $('#hid_DLid').val(), Reason: $('#V_Notes').val(), startDate: start, endDate: end, mondaySelected: $('#dMondaySelected').is(':checked'), tuesdaySelected: $('#dTuesdaySelected').is(':checked'), wednesdaySelected: $('#dWednesdaySelected').is(':checked'), thursdaySelected: $('#dThursdaySelected').is(':checked'), fridaySelected: $('#dFridaySelected').is(':checked'), entireRange: disablerange },
                    success: function (data) {
                        if (document.getElementById("hid_DLid").value == "") {
                            alert("Save Successful")
                            location.reload(true);
                        }
                        else {
                            getLisingInfoTable(start, end)
                            $('#loading-image').hide();
                            alert("Save Successful");
                        }

                    },
                    //complete: function(){
                    //    $('#loading-image').hide();
                    //},
                    error: function (data) {
                        alert("Unable to save list rules, Please review.");
                    }
                });
            }

        //}
        else {
            x = "You pressed Cancel!";
        }

    });

    //$('#loading-image').html('<input type="image" class="AjaxGif" />');

    //$('#loading-image').bind('ajaxStart', function () {
    //    $(this).show();
    //}).bind('ajaxStop', function () {
    //    $(this).hide();
    //});


    //$("#dt1").datepicker({
    //    dateFormat: "dd-M-yy",
    //    minDate: 0,
    //    onSelect: function (date) {
    //        var date2 = $('#dt1').datepicker('getDate');
    //        date2.setDate(date2.getDate() + 1);
    //        $('#dt2').datepicker('setDate', date2);
    //        //sets minDate to dt1 date + 1
    //        $('#dt2').datepicker('option', 'minDate', date2);
    //    }
    //});
    //$('#dt2').datepicker({
    //    dateFormat: "dd-M-yy",
    //    onClose: function () {
    //        var dt1 = $('#dt1').datepicker('getDate');
    //        console.log(dt1);
    //        var dt2 = $('#dt2').datepicker('getDate');
    //        if (dt2 <= dt1) {
    //            var minDate = $('#dt2').datepicker('option', 'minDate');
    //            $('#dt2').datepicker('setDate', minDate);
    //        }
    //    }
    //});

    //$(function () {
    //    $('#ListFromDate, #ListToDate').datepicker({
    //        showOn: "both",
    //        beforeShow: customRange,
    //        dateFormat: "dd M yy",
    //        firstDay: 1,
    //        changeFirstDay: false
    //    });

    //});

    //function customRange(input) {
    //    var min = new Date(2008, 11 - 1, 1), //Set this to your absolute minimum date
    //        dateMin = min,
    //        dateMax = null,
    //        dayRange = 6; // Set this to the range of days you want to restrict to

    //    if (input.id === "ListFromDate") {
    //        if ($("#ListToDate").datepicker("getDate") != null) {
    //            dateMax = $("#ListToDate").datepicker("getDate");
    //            dateMin = $("#ListToDate").datepicker("getDate");
    //            dateMin.setDate(dateMin.getDate() - dayRange);
    //            if (dateMin < min) {
    //                dateMin = min;
    //            }
    //        }
    //        else {
    //            dateMax = new Date; //Set this to your absolute maximum date
    //        }
    //    }
    //    else if (input.id === "ListToDate") {
    //        dateMax = new Date; //Set this to your absolute maximum date
    //        if ($("#ListFromDate").datepicker("getDate") != null) {
    //            dateMin = $("#ListFromDate").datepicker("getDate");
    //            var rangeMax = new Date(dateMin.getFullYear(), dateMin.getMonth(), dateMin.getDate() + dayRange);

    //            if (rangeMax < dateMax) {
    //                dateMax = rangeMax;
    //            }
    //        }
    //    }
    //    return {
    //        minDate: dateMin,
    //        maxDate: dateMax
    //    };
    //}
});
