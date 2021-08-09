$(document).ready(function () {
  
   
    $(".multiselect").multiselect(
    {
        selectedText: "# of # selected",
        header: "Diaries",
        minWidth: 10
        //header: "Choose an Option!"
    });

    $('.ui-multiselect').css('width', '548px');
    $('.ui-multiselect-single').css('width', '150px');
    $('.ui-multiselect-menu').css('width', '548px');

    //$("#tabsLists1").tabs();
    //$("#tabsDetails1").tabs();
    $("#tabsYearDates").tabs();

  //  var v = $("#tabsLists").width();


    //$(".ListTypeDiary").click(function () {
    //    alert($(this).attr('id'));
    //    //  window.location.href = '/DiaryManagement/Edit/' + $(this).attr('id');

    //});

    //$(".tableLists").click(function () {
    //    alert("week = " + $(this).attr('data-listdayhdr1') + "  Day = " + $(this).attr('data-listdayhdr2'));
    //    //  window.location.href = '/DiaryManagement/Edit/' + $(this).attr('id');

    //});
    
 

    //$(".DiaryRec").click(function () {
    //    alert("diarymng = " + $(this).find(".DiaryMng").text());
    //});
    


 
 
  
    $('.color').colorpicker();
    

    $(".datepicker").datepicker({
        changeMonth: true,
        //changeYear: true,
        //showOtherMonths: true,
        //selectOtherMonths: true,
        //dateFormat: 'DD, d MM, yy',
        altField: '#date_due',
        altFormat: 'yy-mm-dd',
        firstDay: 1, // rows starts on Monday 11/7/2014
        dateFormat: "dd-mm-yy",
        timeFormat: "hh:mm tt"
        //numberOfMonths: 3
        }).change(function () {
            if (this.id == 'DN_Start_Date') {

                if ((Date.parse(startDate) >= Date.parse(endDate))) {
                    document.getElementById("DN_End_Date").value = startDate;
                }
            }
    });

    $(".datepickeryear").datepicker({
        changeMonth: true,
        changeYear: true,
        //showOtherMonths: true,
        //selectOtherMonths: true,
        //dateFormat: 'DD, d MM, yy',
        altField: '#date_due',
        altFormat: 'yy-mm-dd',
        firstDay: 1, // rows starts on Monday 11/7/2014
        dateFormat: "dd-mm-yy",
        timeFormat: "hh:mm tt"
    })

    $("#CurrentEndDate").change(function () {
        var startDate = document.getElementById("CurrentStartDate").value;
        var endDate = document.getElementById("CurrentEndDate").value;

        if ((Date.parse(endDate) < Date.parse(startDate))) {
            alert("Current End date should be greater than Start date");
            document.getElementById("CurrentEndDate").value = startDate;
    }
    });

    $("#CurrentStartDate").change(function () {
        var startDate = document.getElementById("CurrentStartDate").value;
        var endDate = document.getElementById("CurrentEndDate").value;

        if ((Date.parse(startDate) >= Date.parse(endDate))) {
            document.getElementById("CurrentEndDate").value = startDate;
            }
    });

    $("#NextEndDate").change(function () {
        var startDate = document.getElementById("NextStartDate").value;
        var endDate = document.getElementById("NextEndDate").value;

        if ((Date.parse(endDate) < Date.parse(startDate))) {
            alert("Next End date should be greater than Start date");
            document.getElementById("NextEndDate").value = startDate;
            }
    });

    $("#NextStartDate").change(function () {
        var startDate = document.getElementById("NextStartDate").value;
        var endDate = document.getElementById("NextEndDate").value;

        if ((Date.parse(startDate) >= Date.parse(endDate))) {
            document.getElementById("NextEndDate").value = startDate;
        }
    });
    $(".multiselect").multiselect(
        {
        selectedText: "# of # selected",
        header: "Diaries",
        minWidth: 10
        //header: "Choose an Option!"
    });

    $('.ui-multiselect').css('width', '548px');
    $('.ui-multiselect-single').css('width', '150px');
    $('.ui-multiselect-menu').css('width', '548px');
       
    


      $(".datepickerList").datepicker({
          changeMonth: true,
          //changeYear: true,
          //showOtherMonths: true,
          //selectOtherMonths: true,
          //dateFormat: 'DD, d MM, yy',
          altField: '#date_due',
          altFormat: 'yy-mm-dd',
          firstDay: 1, // rows starts on Monday 11/7/2014
          dateFormat: "dd-mm-yy",
          timeFormat: "hh:mm tt",
      })

   

    $('#D_L_Default_StartTime ').timepicker()
        .change(function () {
            if (this.id == 'StartTime') {
                //checkStartTime();
                $('#StartTimeDisp').text($(this).val());
                var startValue = $('#StartDate').val() + ' ' + $(this).val();
                $('#StartDateTime').val(startValue);
            }
            else {
               // checkEndTime();
                $('#EndTimeDisp').text($(this).val());
                var endValue = $('#EndDate').val() + ' ' + $(this).val();
                $('#EndDateTime').val(endValue);
            }
        });


    $("#UsersDataList").click(function () {

        $.getJSON($('#UserName').attr("data-autocompleteme"), function (data) {
            var items;
            $.each(data, function (i, alrs) {
                items += "<option>" + data[i] + "</option>";
            });
            $('#UserName').html(items);
        });

    });

    $('#accessgroups').dataTable
        ({
            "bLengthChange": false,
            "bPaginate": true,
            "sScrollY": 400,
            "bJQueryUI": true,
            "sPaginationType": "full_numbers",

        })
            .rowGrouping({
                iGroupingColumnIndex: 0,
                iGroupingOrderByColumnIndex: 0,
                iGroupingOrderByColumnIndex: 0,
                bExpandableGrouping: true,
                iExpandGroupOffset: 1,
                bExpandableGrouping2: true,
                iExpandGroupOffset: 2,
            });

    $('#caseoutcomes').dataTable
        ({
            "bLengthChange": false,
            "bPaginate": true,
            "sScrollY": 400,
            "bJQueryUI": true,
            "sPaginationType": "full_numbers",

        })
            .rowGrouping({
                iGroupingColumnIndex: 0,
                iGroupingOrderByColumnIndex: 0,
                iGroupingOrderByColumnIndex: 0,
                bExpandableGrouping: true,
                iExpandGroupOffset: 1,
                bExpandableGrouping2: true,
                iExpandGroupOffset: 2,
            });

    $('#vacatereasons').dataTable
        ({
            "bLengthChange": false,
            "bPaginate": true,
            "sScrollY": 400,
            "bJQueryUI": true,
            "sPaginationType": "full_numbers",

        })
            .rowGrouping({
                iGroupingColumnIndex: 0,
                iGroupingOrderByColumnIndex: 0,
                iGroupingOrderByColumnIndex: 0,
                bExpandableGrouping: true,
                iExpandGroupOffset: 1,
                bExpandableGrouping2: true,
                iExpandGroupOffset: 2,
            });

    $("#JudicialOfficersDataList").click(function () {

        $.getJSON($('#JO_Name').attr("data-autocompleteme"), function (data) {
            var items;
            $.each(data, function (i, alrs) {
                items += "<option>" + data[i] + "</option>";
            });
            $('#JO_Name').html(items);
        });

    });

    $('#judicialofficers').dataTable
        ({
            "bLengthChange": false,
            "bPaginate": true,
            "sScrollY": 400,
            "bJQueryUI": true,
            "sPaginationType": "full_numbers",

        });

    $('#judicialofficersListings').dataTable
        ({
            "bLengthChange": false,
            "bPaginate": true,
            "sScrollY": 400,
            "bJQueryUI": true,
            "sPaginationType": "full_numbers",

        });
    // ************* Datatables Server Side ********************/
    TableTools.BUTTONS.Back = $.extend(true, TableTools.buttonBase, {
        "sAction": "div",
        "sTag": "default",
        "sToolTip": "Back to main list",
        "sNewLine": " ",
        "sButtonText": "Back to List",
        "fnClick": function (nButton, oConfig) {
            document.location.href = window.location.href;
        }
    });


    var oTable = $('#daynotes').dataTable({

        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            $(nRow).on('click', function () {
                document.location.href = window.location.href.split('?')[0] + "/Edit/" + aData[5] + "?" + window.location.href.split('?')[1];
            })
        },

        "sDom": 'T<"clear">lfrtip',
        "oTableTools": {
            "aButtons": [
                    {
                        "sExtends": "Back",
                        "sButtonText": "Back to List"
                    },
            ]

        },

        "bLengthChange": false,
        "bPaginate": false,
        "bServerSide": true,
        "sAjaxSource": "Daynote/GetAjaxData",
        "bProcessing": true,
        "bFilter": true,
        "aoColumn": [

        { "sName": "Status" },
        { "sName": "Year" },
        { "sName": "Start Date" },
        { "sName": "End Date" },
        { "sName": "Day Note" },
        { "sName:": "Id", "bSearchable": false, "bVisible": false }

        ]
    })
      .rowGrouping({
          iGroupingColumnIndex2: 1,
    });

    $('#daynotes_filter input').unbind();

    $('#daynotes_filter input').bind('keyup', function (e) {
        if ($(this).val().length > 2) {
            oTable.fnFilter(this.value);
        }
    });

    $('#daynotes tbody').on('click', 'td.subgroup', function () {
        oSettings = oTable.fnSettings();

        value2 = $(this).html().toLowerCase();
        test5 = value2.replace(/ /g, "").replace(/-/g, "");
        test1 = $(this).attr("class");
        test3 = test1.replace(/(subgroup|-)/g, "");
        test4 = test3.replace(test5, "");
        value1 = test4.replace(/ /g, "").replace(/-/g, "");


        if (oSettings != null) {
            oSettings.aoServerParams.push({
                "sName": "user",
                "fn": function (aoData) {
                    aoData.push(
                        { "name": "first_data", "value": value1 },
                        { "name": "second_data", "value": value2 }
                        );
                }
            });

            oTable.fnDraw();
        }
    });

    var onsdTable = $('#nsdays').dataTable({

        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            $(nRow).on('click', function () {
                document.location.href = window.location.href.split('?')[0] + "/Edit/" + aData[4] + "?" + window.location.href.split('?')[1];
            })
        },

        "sDom": 'T<"clear">lfrtip',
        "oTableTools": {
            "aButtons": [
                    {
                        "sExtends": "Back",
                        "sButtonText": "Back to List"
                    },
            ]

        },

        "bLengthChange": false,
        "bPaginate": false,
        "bServerSide": true,
        "sAjaxSource": "NonSittingDay/GetAjaxData",
        "bProcessing": true,
        "bFilter": true,
        "aoColumn": [

        { "sName": "Year" },
        { "sName": "Status" },
        { "sName": "Date" },
        { "sName": "Reason" },
        { "sName:": "Id", "bSearchable": false, "bVisible": false }

        ]
    })
      .rowGrouping({
          iGroupingColumnIndex2: 1,
      });

    $('#nsdays_filter input').unbind();

    $('#nsdays_filter input').bind('keyup', function (e) {
        if ($(this).val().length > 2) {
            onsdTable.fnFilter(this.value);
        }
    });

    $('#nsdays tbody').on('click', 'td.subgroup', function () {
        oSettings = onsdTable.fnSettings();

        value2 = $(this).html().toLowerCase();
        test5 = value2.replace(/ /g, "").replace(/-/g, "");
        test1 = $(this).attr("class");
        test3 = test1.replace(/(subgroup|-)/g, "");
        test4 = test3.replace(test5, "");
        value1 = test4.replace(/ /g, "").replace(/-/g, "");


        if (oSettings != null) {
            oSettings.aoServerParams.push({
                "sName": "user",
                "fn": function (aoData) {
                    aoData.push(
                        { "name": "first_data", "value": value1 },
                        { "name": "second_data", "value": value2 }
                        );
                }
            });

            onsdTable.fnDraw();
        }
    });

    /* hide initially */
    //if ($("#EventType").val() == "NJCC") {
    //    $("#Organisation").hide();
    //    $("#OrganisationTitle").hide();
    //}

    //$("#EventType").change(function () {
    //    if ($(this).val() == "Community") {
    //        $("#Organisation").show();
    //        $("#OrganisationTitle").show();
    //    }
    //    else {
    //        $("#Organisation").val("");
    //        $("#Organisation").hide();
    //        $("#OrganisationTitle").hide();
    //    }
    //});

    //$("#EventName").change(function () {
    //    $('#EventTimeDisp').text($(this).val() + " ----- " + $('#Aim').val());
    //});

    //$("#Aim").change(function () {
    //    $('#EventTimeDisp').text($('#EventName').val() + " ----- " + $(this).val());
    //});


    //var result = $('#Attachments').data('njcc-read');
    //if (result == 'True')
    //    var res = true;
    //else
    //    var res = false;
    //// alert(result);
    //tinymce.init(
    //{
    //    selector: "textarea",
    //    readonly: res,
    //    theme: "modern",
    //    plugins: [
    //        "advlist autolink lists link image charmap print preview hr anchor pagebreak",
    //        "searchreplace wordcount visualblocks visualchars code fullscreen",
    //        "insertdatetime media nonbreaking save table contextmenu directionality",
    //        "emoticons template paste textcolor"
    //    ],
    //    toolbar1: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
    //    toolbar2: "print preview media | forecolor backcolor emoticons",
    //    image_advtab: true
       
    //});


    //$('#accessgroups').dataTable
    //    ({
    //        "bLengthChange": false,
    //        "bPaginate": true,
    //        "sScrollY": 400,
    //        "bJQueryUI": true,
    //        "sPaginationType": "full_numbers",

    //    })
    //        .rowGrouping({
    //            iGroupingColumnIndex: 0,
    //            iGroupingOrderByColumnIndex: 0,
    //            //iGroupingColumnIndex2: 1,
    //            iGroupingOrderByColumnIndex: 0,
    //            bExpandableGrouping: true,
    //            iExpandGroupOffset: 1,
    //            bExpandableGrouping2: true,
    //            iExpandGroupOffset: 2,
    //        });

    //$("#UsersDataList").click(function () {

    //    $.getJSON($('#UserName').attr("data-autocompleteme"), function (data) {
    //        var items;
    //        $.each(data, function (i, alrs) {
    //            items += "<option>" + data[i] + "</option>";
    //        });
    //        $('#UserName').html(items);
    //    });

    //});
    //$("#StaffContactDataList").click(function () {

    //    $.getJSON($('#StaffContact').attr("data-autocompleteme"), function (data) {
    //        var items;
    //        $.each(data, function (i, alrs) {
    //            items += "<option>" + data[i] + "</option>";
    //        });
    //        $('#StaffContact').html(items);
    //    });

    //});

    //$(function () {

    //    var options = {
    //        "appIconUrl": 'https://dev-sccm/njcc/Images/njccEventPic.JPG',
    //        "appTitle": "NJCC Events",
    //        "appHelpPageUrl": "Help.html?"
    //            + document.URL.split("?")[1],
    //        "settingsLinks": [
    //            {
    //                "linkUrl": "Account.html?"
    //                    + document.URL.split("?")[1],
    //                "displayName": "Account settings"
    //            },
    //            {
    //                "linkUrl": "Contact.html?"
    //                    + document.URL.split("?")[1],
    //                "displayName": "Contact us"
    //            }
    //        ]
    //    };


    //    var nav = new SP.UI.Controls.Navigation("chrome_ctrl_container", options);
    //    nav.setVisible(true);
    //});

    //$(".datepicker").datepicker({
    //    changeMonth: true,
    //    changeYear: true,
    //    showOtherMonths: true,
    //    selectOtherMonths: true,
    //    //dateFormat: 'DD, d MM, yy',
    //    altField: '#date_due',
    //    altFormat: 'yy-mm-dd',
    //    firstDay: 1, // rows starts on Monday 11/7/2014
    //    dateFormat: "dd-mm-yy",
    //    timeFormat: "hh:mm tt"
    //}).change(function () {
    //    if (this.id == 'EndDate') {
    //        if ($('#EndDate').val() <= $('#StartDate').val()) {
    //            var endValue1 = $('#EndDate').val();
    //            $('#StartDate').val(endValue1);
    //        }
    //        checkEndTime();
    //        var endValue1 = $(this).val() + ' ' + $('#EndTime').val();;
    //        $('#EndDateTime').val(endValue1);
    //    }
    //    else
    //        if (this.id == 'StartDate') {
    //            if ($('#EndDate').val() <= $('#StartDate').val()) {
    //                var startValue1 = $('#StartDate').val();
    //                $('#EndDate').val(startValue1);
    //            }
    //            checkStartTime();
    //            var startValue1 = $(this).val() + ' ' + $('#StartTime').val();
    //            $('#StartDateTime').val(startValue1);
    //        }
    //});




    //function checkEndTime() {
    //    //   alert( $('#EndTime').val() + " -- " + $('#StartTime').val());
    //    if ($('#EndDate').val() == $('#StartDate').val()) {
    //        var from = Date.parse($('#StartDate').val() + ' ' + $('#StartTime').val());
    //        var to = Date.parse($('#EndDate').val() + " " + $('#EndTime').val());
    //        if (from > to) {    // if ($('#StartTime').val() > $('#EndTime').val()) {         
    //            var endValue = $('#EndTime').val();
    //            $('#StartTime').val(endValue);
    //            $('#StartTimeDisp').text(endValue);
    //        }
    //    }
    //};

    //function checkStartTime() {
    //    if ($('#EndDate').val() == $('#StartDate').val()) {
    //        var from = Date.parse($('#StartDate').val() + ' ' + $('#StartTime').val());
    //        var to = Date.parse($('#EndDate').val() + " " + $('#EndTime').val());
    //        // alert($('#StartDate').val() + ' ' + $('#StartTime').val() + " -- " + $('#EndDate').val() + " " + $('#EndTime').val());
    //        if (from > to) {    //if ($('#StartTime').val() > $('#EndTime').val()) {
    //            // alert(from + " > " + to);        
    //            var startValue = $('#StartTime').val();
    //            $('#EndTime').val(startValue);
    //            $('#EndTimeDisp').text(startValue);
    //        }
    //    }
    //};



    //$("#tabsLists").resizable({      
    //    minHeight: 150,
    //    maxHeight: 600,
    //    minWidth: 1230,
    //    maxWidth: 1230,
    //    alsoResize: "#tabsDetails"
    //start: function () {
    //    //var that = $(this).resizable( "instance" ),
    //    //    o = that.options,
    //    //    _store = function (exp) {
    //    //        $(exp).each(function() {
    //    //            var el = $(this);
    //    //            el.data("ui-resizable-alsoresize", {
    //    //                width: parseInt(el.width(), 10), height: parseInt(el.height(), 10),
    //    //                left: parseInt(el.css("left"), 10), top: parseInt(el.css("top"), 10)

    //    //            });
    //    //        });
    //    //    };
    //    //if (typeof(o.alsoResize) === "object" && !o.alsoResize.parentNode) {
    //    //    if (o.alsoResize.length) { o.alsoResize = o.alsoResize[0]; _store(o.alsoResize); }
    //    //    else { $.each(o.alsoResize, function (exp) { _store(exp); }); }
    //    //}else{
    //    //    _store(o.alsoResize);
    //    //}
    //},

    //resize: function (event, ui) {

    //    //var that = $(this).resizable( "instance" ),
    //    //    o = that.options,
    //    //    os = that.originalSize,
    //    //    op = that.originalPosition,
    //    //    delta = {
    //    //        //height: (that.size.height - os.height) || 0, width: (that.size.width - os.width) || 0,
    //    //        //top: (that.position.top - op.top) || 0, left: (that.position.left - op.left) || 0
    //    //        height: (that.size.height - os.height) || 0, width: (that.size.width - os.width) || 0,
    //    //        top: (that.position.top - op.top) || 0, left: (that.position.left - op.left) || 0

    //    //    },

    //    //    _alsoResize = function (exp, c) {
    //    //        $(exp).each(function() {
    //    //            var el = $(this), start = $(this).data("ui-resizable-alsoresize"), style = {},
    //    //                css = c && c.length ? c : el.parents(ui.originalElement[0]).length ? ["width", "height"] : ["width", "height", "top", "left"];


    //    //            $.each(css, function (i, prop) {
    //    //                var sum = (start[prop]||0) - (delta[prop]||0);
    //    //                if (sum && sum >= 0) {
    //    //                    style[prop] = sum || null;
    //    //                }
    //    //            });

    //    //            el.css(style);

    //    //        });
    //    //    };

    //    //if (typeof(o.alsoResize) === "object" && !o.alsoResize.nodeType) {
    //    //    $.each(o.alsoResize, function (exp, c) { _alsoResize(exp, c); });
    //    //}else{
    //    //    _alsoResize(o.alsoResize);
    //    //}
    //},

    //stop: function () {
    //    //$(this).removeData("resizable-alsoresize");
    //    $('#tabsLists').css('width', '100%');
    //    $('#tabsDetails').css('width', '100%');
    //}

    //});
});