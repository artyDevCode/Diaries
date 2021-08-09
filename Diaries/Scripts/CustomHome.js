$(function () {
    $("#tabsLists1").tabs();
    $("#tabsDetails1").tabs();

    $(".tableListHdr").click(function () {
        hideDaysDetails();      
        $("#TableDetails" + $(this).attr('data-listdayhdr1')).show();
    });

    $(".ListTypeDiary").click(function () {
        alert($(this).attr('id'));
    });

    $(".DiaryRec").click(function () {
        alert("diarymng = " + $(this).find(".DiaryMng").text());
    });

    $(document).on("click", "a[href=#tabs-2]", function () {
        hideDaysList();
        hideDaysDetails();
        var date = $(".datepickerMain").datepicker('getDate');
        date = $.datepicker.formatDate("DD", date);
        $("#TableList" + date).show();
        $("#TableDetails" + date).show();
    });


    $(".datepickerMain").datepicker({
        changeMonth: true,
        changeYear: true,
        autoSize: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        altField: '#date_due',
        altFormat: 'yy-mm-dd',
        firstDay: 1, // rows starts on Monday 11/7/2014
        dateFormat: "dd-mm-yy",
        timeFormat: "hh:mm tt",
        numberOfMonths: [2, 1],
        Default: ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"]
    });

    var sizeHistory = "";

    $("#tabsLists").resizable({
        minHeight: 150,
        maxHeight: 600,
        minWidth: 1230,
        maxWidth: 1230,
        stop: function () {
            sizeHistory = JSON.stringify({ width: this.style.width, height: this.style.height });
        }
    });  


    $(document).on("click", "#NextListSelectionList", function () {
        var curTab1 = $('#tabsLists1').tabs('option', 'active');
        nextprevClick(curTab1,'plus');      
    });
 
    $(document).on("click", "#PrevListSelectionList", function () {
        var curTab2 = $('#tabsLists1').tabs('option', 'active');
        nextprevClick(curTab2, 'minus');   
    });

    $(document).on("click", "#NextListSelectionDetail", function () {
        var curTab1 = $('#tabsDetails1').tabs('option', 'active');
        nextprevClick(curTab1 = curTab1 == true ? false : true, 'plus'); //switch the value around since week is not the first tab
    });

    $(document).on("click", "#PrevListSelectionDetail", function () {
        var curTab2 = $('#tabsDetails2').tabs('option', 'active');
        nextprevClick(curTab2 = curTab2 == true ? false : true, 'minus');  //switch the value around since week is not the first tab
    });

    $(document).on("change", ".datepickerMain", function () {
        callAjax($(this).val());   
        return false;
    });


    function nextprevClick(curTabs, action) {
        var date = $(".datepickerMain").datepicker('getDate');
        if (curTabs == 0) {
            if (action == 'plus')
                date.setDate(date.getDate() + 7);
            else
                date.setDate(date.getDate() - 7);
            $('.datepickerMain').datepicker('setDate', date);
            date = $.datepicker.formatDate("dd-mm-yy", date);
            callAjax(date);
           return false;
        }
        else {
            var date = $('.datepickerMain').datepicker('getDate');
            hideDaysList();
            hideDaysDetails();
            var day = date.getUTCDay();
            if (action == 'plus') {
                if (date.getUTCDay() != 4)
                    date.setDate(date.getDate() + 1);
                else
                    date.setDate(date.getDate() - 4);
            }
            else {
                if (date.getUTCDay() != 0)
                    date.setDate(date.getDate() - 1);
                else
                    date.setDate(date.getDate() + 4);
            }
            $('.datepickerMain').datepicker('setDate', date);
            date = $.datepicker.formatDate("DD", date);

            $("#TableList" + date).show();
            $("#TableDetails" + date).show();         
        }
    };

    function callAjax(date) {

        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "Home/Index";
        }
        else {
            pathURL = pathArray[3] + "/" + pathArray[4] + "/Home/Index";
        }

        //alert(pathURL);

        var options = {
            url: pathURL, //'Home/Index',
            type: "GET",
          // resetForm: true,
            data: { term: date }
        };
        $.ajax(options).done(function (result) {
            var $target = $("#article");
            var $newHtml = $(result);
            $target.replaceWith($newHtml);
            $("#tabsLists1").tabs();
            $("#tabsDetails1").tabs();
            var ta_size = sizeHistory;
            if (ta_size == "") ta_size = { width: '100%', height: '300px' };
            else
                ta_size = JSON.parse(ta_size);
            $("#tabsLists").css(ta_size);
            $("#tabsLists").resizable({
                minHeight: 150,
                maxHeight: 600,
                minWidth: 1230,
                maxWidth: 1230,
                stop: function () {
                    sizeHistory = JSON.stringify({ width: this.style.width, height: this.style.height });
                }
            });
            $("#tabsLists").css({})

            $(".tableListHdr").click(function () {
                hideDaysDetails();
                $("#TableDetails" + $(this).attr('data-listdayhdr1')).show();
            });
        });
    };

    function hideDaysList() {
        $("#TableListMonday").hide();
        $("#TableListTuesday").hide();
        $("#TableListWednesday").hide();
        $("#TableListThursday").hide();
        $("#TableListFriday").hide();
    };

    function hideDaysDetails() {
        $("#TableDetailsMonday").hide();
        $("#TableDetailsTuesday").hide();
        $("#TableDetailsWednesday").hide();
        $("#TableDetailsThursday").hide();
        $("#TableDetailsFriday").hide();
    };
});
