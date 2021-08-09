$.ajaxSetup({ cache: false });

$(function () {

    var selectedDiaryName;
    var selectedListId;
    var pathArray;
    var pathURL;
    var selectListAttrType = "Vacate";
    var VRFilter="All";
    var OFilter = "All";
    var ListSubActionSelected = false;
    var fid = 0;
    var lid = 0;
    var cid = 0;

    $('.color').colorpicker();

    $('#D_L_Default_StartTime ').timepicker()
    .change(function () {
        if (this.id == 'D_L_Default_StartTime') {
            //checkStartTime();
            $('#StartTimeDisp').text($(this).val());
            var startValue = $('#StartDate').val() + ' ' + $(this).val();
            $('#D_L_Default_StartTime').val(startValue);
        }
        else {
            // checkEndTime();
            $('#EndTimeDisp').text($(this).val());
            var endValue = $('#EndDate').val() + ' ' + $(this).val();
            $('#EndDateTime').val(endValue);
        }
    });


    function getFirstListId() {
        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/DiaryManagement/GetDiaryListId";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/GetDiaryListId";
        }
        $.ajax({
            // Get List Types PartialView
            url: pathURL,
            type: 'Get',
            data: { diaryName: selectedDiaryName },
            success: function (data) {
                selectedListId = data
            },
            error: function () {
                alert("There has been an error loading list details, please refresh view.");
            }
        });
    }

    function getListTypesTable() {
        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/DiaryManagement/DiaryToListTypes";
        }
        else
        {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/DiaryToListTypes";
        }

        $.ajax({
            // Get List Types PartialView
            url: pathURL,
            type: 'Get',
            data: { diaryName: selectedDiaryName },
            success: function (data) {
                $("#ListTypesDetailTable").empty().append(data);
                //selectedListId = data.
            },
            //error: function () {
            error: function (xhr, textStatus, errorThrown) {
                //alert(xhr.responseText);
                alert("There has been an error loading list types, please retry selection.");
            }
        });
    }

    function getDiaryDetails() {

        //pathName = window.location.hostname;
        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/DiaryManagement/GetDiaryDetails";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/GetDiaryDetails";
        }

        $.ajax({
            // Get Diary Details PartialView
            url: pathURL, //'@Url.Action("GetDiaryDetails", "DiaryManagement")', //"/DiaryManagement/GetDiaryDetails",
            type: 'Get',
            data: { diaryName: selectedDiaryName },
            success: function (data) {
                $("#DiaryDetailTable").empty().append(data);
                $('.color').colorpicker();
            },
            //error: function () {
            error: function (xhr, textStatus, errorThrown) {
                //alert(xhr.responseText);
                //alert(document.getElementById('DiariesSection').innerHTML = xhr.responseText);
                alert("There has been an error loading diary details, please retry selection.");
            }
        
        });
        
    }

    function getListDetails() {
        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/DiaryManagement/GetListDetails";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/GetListDetails";
        }

        $.ajax({
            // Get List Details PartialView
            url: pathURL,
            type: 'Get',
            data: { listId: selectedListId, diaryName: selectedDiaryName},
            success: function (data) {
                $("#ListDetailTable").empty().append(data);
                $('.color').colorpicker();
                $('#D_L_Default_StartTime ').timepicker()
            },
            error: function () {
                alert("There has been an error loading diary list details, please retry selection.");
            }
        });

    }

    function getListAttrDetails() {
        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/DiaryManagement/GetListAttrDetails";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/GetListAttrDetails";
        }

        $.ajax({
            // Get List Attribute Details PartialView
            url: pathURL,
            type: 'Get',
            data: { listId: selectedListId, diaryName: selectedDiaryName },
            success: function (data) {
                $("#ListAttrsDetailTable").empty().append(data);
                getListVacateReasonDetails();
                $("#ListVacateDetailTable").fadeIn();
                //document.getElementById("VacateReasonSelected").style.backgroundColor = '#FFFF00';
                //$('.color').colorpicker();
                //$('#D_L_Default_StartTime ').timepicker()
            },
            error: function () {
                //alert("There has been an error loading list attribute details, please retry selection.");
            }
        });

    }

    //$("#DiariesSection").fadeOut(0);
    //$("#ListTypesDetailSection").fadeOut(0);

    //$("#ListTypesDetailTable").click(function () {
    //    alert('ListTypesDetailTable')
    //    selectedListName = $(this).find(".Listings").text().trim();
    //    alert($(".Listings").attr('id'));
    //    alert(selectedListName);
    //});
    
  //  $("#ListTypesDetailTable").on('click', '.Listings', function () {
   
  
    var DELAY = 250, clicks = 0, timer = null;

    //$("#ListTypesDetailTable").click(function (e) {
    $("#ListTypesDetailTable").on('click', '.Listings', function (e) {

        //if (ListSubActionSelected == false)
        //{
        selectedListId = $(this).attr('id');
        //selectedListId = $(Listings).find(".ListMngId").text().trim();
        selectedListName = $(this).find(".ListMng").text().trim();

        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/ListType/Edit/?listId=" + selectedListId + "&diaryName=" + selectedDiaryName;
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/ListType/Edit/?listId=" + selectedListId + "&diaryName=" + selectedDiaryName;
        }

        if (timer == null) {
            timer = setTimeout(function () {
                clicks = 0;
                timer = null;
                //alert("Single");
                // single click code
                    //selectedListId = $(this).attr('id');
                    //selectedListName = $(this).find(".ListMng").text().trim();
                    //alert(ListSubActionSelected);

                    if (selectedListId != "") {
                        getListDetails();
                        $("#ListDetailSection").fadeIn();

                        getListAttrDetails();
                        $("#ListAttrsDetailTable").fadeIn();
                    }
            }, DELAY);
        }

        if (clicks === 1) {
            clearTimeout(timer);
            timer = null;
            clicks = -1;
            //alert("Double");
            // double click code
            var GoToPage = pathURL;
            window.location = GoToPage;

        }
        clicks++;
        //}
    });

    //$("#ListTypesDetailTable").on('click', '.Listings', function () {
    //    selectedListId = $(this).attr('id');
    //    selectedListName = $(this).find(".ListMng").text().trim();
    //    //alert(selectedListId); 
    //    if (selectedListId != "") {
    //        getListDetails();
    //        $("#ListDetailSection").fadeIn();

    //        getListAttrDetails();
    //        $("#ListAttrsDetailTable").fadeIn();

    //    }
    //});

    //$("#ListTypesDetailTable").dblclick(function (event) {
    //    $("#ListTypesDetailTable").click().click();
    //});


    //$(".ListTypesDetailTable").init(function () {
    //    selectedListId = $(this).attr('id');
    //    selectedListName = $(this).find(".ListMng").text().trim();
    //    //alert(selectedListId); 
    //    if (selectedListId != "") {
    //        getListDetails();
    //        $("#ListDetailSection").fadeIn();
    //    }
    //});


    $("#ListTypesSection").click(function () {
        //alert('ListTypesSection')
    });
    

    $("#DiaryListings").click(function () {
        alert('DiaryListings')
    });

    $("#Listings").click(function () {
        alert('Listings')
    });


    $(".DiaryManage").click(function () {
        //alert("diarymanage = " + $(this).find(".DiaryMng").val());
        selectedDiaryName = $(this).find(".DiaryMngId").text().trim();
        selectedListId = "0";

        //alert("diarymanageclick = " + selectedDiaryName);

        if (selectedDiaryName != "") {
            getDiaryDetails();
            $("#DiaryDetailSection").fadeIn();

            getListTypesTable();
            $("#ListTypesDetailSection").fadeIn();

            getListDetails();
            $("#ListDetailSection").fadeIn();

            getListAttrDetails();
            $("#ListAttrsDetailTable").fadeIn();

            //var element = document.getElementById('D_Fields');
            //element.value = "KIDS";
        }

    });

    $(".DiaryManage").init(function () {
        selectedDiaryName = $(this).find(".DiaryMngId").text().trim();
        selectedDiaryName = selectedDiaryName.substring(0, 2).trim();
        //alert("diarymanage = " + selectedDiaryName);

        if (selectedDiaryName != "") {

            getDiaryDetails();
            $("#DiaryDetailSection").fadeIn();

            //alert("after getDiaryDetails");

            getListTypesTable();
            $("#ListTypesDetailSection").fadeIn();

            //alert("after getListTypesTable");

            //getFirstListId(selectedDiaryName);
            //if (selectedListId != "")
            //{
                getListDetails();
            //}
            //alert("after getListDetails");

            getListAttrDetails();
            $("#ListAttrsDetailTable").fadeIn();

            //getListVacateReasonDetails();
            //$("#ListVacateDetailTable").fadeIn();

            //var element = document.getElementById('D_Fields');
            //element.value = "KIDS";

        }

    });

    $("#SaveDiary").click(function () {

        //alert("diarymanage = " + $(this).find(".DiaryMng").val());
        //var selectedDiaryName = document.getElementById("D_DiaryName").value //$(this).find(".D_DiaryName").text();
        //var selectedOutline = $(this).find(".D_Outline").text();
        //var modelDataJSON = '@Html.Raw(Json.Encode(Model))';
        //var ddata =
        //{
        //    Id: $(this).find(".D_id").text(),
        //    Value: "Hello, world!"
        //};

        //var diarylength = $("#D_DiaryName").val().length;

        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/DiaryManagement/EditDiary";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/EditDiary";
        }

        if (document.getElementById("D_DiaryName").value == "")
        {
            alert("Diary name must be entered");
            $("#D_DiaryName").focus();
        }
        if ($("#D_DiaryName").val().length > 30) {
            alert("Diary name must be 30 characters");
            $("#D_DiaryName").focus();
        }
        //else if (document.getElementById("D_Outline").value == "") {
        //    alert("Diary outline must be entered");
        //    $("#D_Outline").focus();
        //}
        else
        {
            $.ajax({
                url: pathURL,
                type: 'Post',
                //data: $('#form').serialize(),                              
                data: { aD_Did: $('#hid_Did').val(), aD_DiaryName: $('#D_DiaryName').val(), aD_Fields: $('#D_Fields').val(), aD_Outline: "Temp", aD_DatePicker: $('#D_DatePickerButton').val(), aStandardUser: "Standard", aD_ReadOnly: "ReadOnly", aD_Bckground_Icon_Colour: $('#D_Bckground_Icon_Colour').val(), aD_Multiday_Cases: $('#D_Multiday_Cases').val(), aD_Dates: $('#D_Dates').val(), aD_Vacated: $('#D_Vacated').val(), aD_ListSelection: $('#D_ListSelection').val() },
                //dataType: 'json',
                success: function (data) {
                    if (document.getElementById("hid_Did").value == "")
                    {
                        alert("Diary" + document.getElementById("D_DiaryName").value + " has been added.");
                        location.reload(true);
                    }
                    else
                    {
                        alert("Save Successful");
                        //alert("Diary " + document.getElementById("D_DiaryName").value + " has been updated.");
                    }
                    
                },
                error: function (data) {
                    alert("Unable to save diary duplicate name exists, Please review.");
                }
            });
        }
        //else {alert("sdf");}
        
    });

    $("#ListingActions").init(function () {
        //alert("listingactions")
    });

    $(document).on('click','#AddList', function () {
        //$("#hid_Did").val("");
        $("#hid_DLid").val("");
        $("#D_L_Lists").val("");
        $("#D_L_ShortName").val("");
        $("#D_L_Lists").focus();
    });

    $(document).on('click', '#SaveList', function () {

        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/DiaryManagement/EditList";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/EditList";
        }

        if (document.getElementById("D_L_Lists").value == "") {
            alert("List name must be entered");
            $("#D_L_Lists").focus();
        }
        else if ($("#D_L_Lists").val().length > 50) {
            alert("List name must be 50 characters");
            $("#D_L_Lists").focus();
        }
        else if (document.getElementById("D_L_ShortName").value == "") {
            alert("List short name must be entered");
            $("#D_L_ShortName").focus();
        }
        else if ($("#D_L_ShortName").val().length > 8) {
            alert("List short name must be 8 characters");
            $("#D_L_ShortName").focus();
        }
        else {

            //$('#D_L_Rules').is(':checked');

            $.ajax({
                url: pathURL,
                type: 'Post',
                data: { aDL_Did: $('#hid_Did').val(), aDL_Dlid: $('#hid_DLid').val(), aDL_DLlist: $('#D_L_Lists').val(), aDL_Shortname: $('#D_L_ShortName').val(), aDL_DLRules: $('#D_L_Rules').is(':checked'), aDL_DLType: $('#D_L_Type').val(), aDL_DLRulesApplyTo: $('#D_L_RulesApplyTo').val(), aDL_DLVacateOptions: $('#D_L_VacateOptions').val(), aDL_DLBckground_row_Colour: $('#D_L_Bckground_row_Colour').val(), aDL_DLText_Colour: $('#D_L_Text_Colour').val(), aDL_DLMandatoryCategory: $('#D_L_MandatoryCategory').is(':checked'), aDL_DLMandatoryDuration: $('#D_L_MandatoryDuration').is(':checked'), aDL_DLMandatoryLocation: $('#D_L_MandatoryLocation').is(':checked') , aDL_DLMandatoryStartTime:  $('#D_L_MandatoryStartTime').is(':checked'), aDL_DLDefault_StartTime: $('#D_L_Default_StartTime').val() },
                success: function (data) {
                    if (document.getElementById("hid_DLid").value == "") {
                        alert("List " + document.getElementById("D_L_ShortName").value + " has been added.");
                        location.reload(true);
                    }
                    else {
                        alert("Save Successful");
                        //alert("List " + document.getElementById("D_L_ShortName").value + " has been updated.");
                    }

                },
                error: function (data) {
                    alert("Unable to save list duplicate name exists, Please review.");
                }
            });
        }

    });

    $(document).on('click', '#CancelList', function () {

        var r = confirm("Do you wish to cancel? No data will be saved");
        if (r == true) {
            pathArray = window.location.href.split('/');
            if (window.location.hostname.indexOf("localhost") >= 0) {
                pathURL = "/DiaryManagement/EditList";
            }
            else {
                pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/EditList";
            }

            $("#hid_DLid").val("cancel");

            $.ajax({
                url: pathURL,
                type: 'Post',
                data: { aDL_Did: $('#hid_Did').val(), aDL_Dlid: $('#hid_DLid').val(), aDL_DLlist: $('#D_L_Lists').val(), aDL_Shortname: $('#D_L_ShortName').val(), aDL_DLRules: $('#D_L_Rules').is(':checked'), aDL_DLType: $('#D_L_Type').val(), aDL_DLRulesApplyTo: $('#D_L_RulesApplyTo').val(), aDL_DLVacateOptions: $('#D_L_VacateOptions').val(), aDL_DLBckground_row_Colour: $('#D_L_Bckground_row_Colour').val(), aDL_DLText_Colour: $('#D_L_Text_Colour').val(), aDL_DLMandatoryCategory: $('#D_L_MandatoryCategory').is(':checked'), aDL_DLMandatoryDuration: $('#D_L_MandatoryDuration').is(':checked'), aDL_DLMandatoryLocation: $('#D_L_MandatoryLocation').is(':checked'), aDL_DLMandatoryStartTime: $('#D_L_MandatoryStartTime').is(':checked'), aDL_DLDefault_StartTime: $('#D_L_Default_StartTime').val() },
                success: function (data) {
                    location.reload(true);
                },
                error: function (data) {
                    alert("Error cancelling list, please refresh view.");
                }
            });
        }
        else
        {
            x = "You pressed Cancel!";
        }



    });

    $("#AddDiary").click(function () {
        $("#hid_Did").val("");
        $("#D_DiaryName").val("");
        $("#D_Outline").val("Temp");
        $("#D_L_ShortName").val("");
        $("#D_L_Lists").val("");
        $("#D_DiaryName").focus();
    });

    $("#CancelDiary").click(function () {

        var r = confirm("Do you wish to cancel? No data will be saved");
        if (r == true) {

            pathArray = window.location.href.split('/');
            if (window.location.hostname.indexOf("localhost") >= 0) {
                pathURL = "/DiaryManagement/EditDiary";
            }
            else {
                pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/EditDiary";
            }

            $("#hid_Did").val("cancel");

            $.ajax({
                url: pathURL,
                type: 'Post',
                data: { aD_Did: $('#hid_Did').val(), aD_DiaryName: $('#D_DiaryName').val(), aD_Fields: $('#D_Fields').val(), aD_Outline: "Temp", aD_DatePicker: $('#D_DatePickerButton').val(), aStandardUser: "Standard", aD_ReadOnly: "ReadOnly", aD_Bckground_Icon_Colour: $('#D_Bckground_Icon_Colour').val(), aD_Multiday_Cases: $('#D_Multiday_Cases').val(), aD_Dates: $('#D_Dates').val(), aD_Vacated: $('#D_Vacated').val(), aD_ListSelection: $('#D_ListSelection').val() },
                success: function (data) {
                    location.reload(true);
                },
                error: function (data) {
                    alert("Error cancelling diary, please refresh view.");
                }
            });
        }
    });

    $(document).on('click', '#VacateReasonSelected', function () {
        //alert("Vacate");
        getListVacateReasonDetails();
        $("#ListOutcomeDetailTable").fadeOut();
        $("#ListFlagDetailTable").fadeOut();
        $("#ListLocationDetailTable").fadeOut();
        $("#ListCategoryDetailTable").fadeOut();
        $("#ListVacateDetailTable").fadeIn();
        document.getElementById("LocationSelected").style.backgroundColor = 'threedlightshadow';
        document.getElementById("CategorySelected").style.backgroundColor = 'threedlightshadow';
        document.getElementById("FlagSelected").style.backgroundColor = 'threedlightshadow';
        document.getElementById("VacateReasonSelected").style.backgroundColor = '#FFFF00';
        document.getElementById("OutcomeSelected").style.backgroundColor = 'threedlightshadow';
    });
        
    function getListVacateReasonDetails() {

        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/DiaryManagement/GetListVacateReasonDetails";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/GetListVacateReasonDetails";
        }

        $.ajax({
            // Get List Vacate Reason Details PartialView
            url: pathURL, 
            type: 'Get',
            data: { listId: selectedListId, diaryName: selectedDiaryName, vrFilter: VRFilter},
            success: function (data) {
                $("#ListVacateDetailTable").empty().append(data);
                $('.color').colorpicker();

                //var inputUser = $("input[name=All]:checked").val()
                //document.getElementsByName("All").checked = true;
                //var radioButtons = document.getElementsByName('All');
                //radioButtons.checked = true;
                //$('#VRFilter').prop('checked', true);
                //btn = document.getElementById("All");
                //btn.checked = true; 
            },
            error: function (xhr, textStatus, errorThrown) {
                //alert(xhr.responseText);
                alert("Error loading list vacate details, please retry selection.");
            }

        });

    }

    $(document).on('change', '#V_VR_SelectName', function () {
        if (document.getElementById("V_VR_SelectName").value != "Select") {
            //alert("clicked");
            UpdateVacateReasons("Add","");
            $("#ListVacateDetailTable").fadeIn();
            $("#ListOutcomeDetailTable").fadeOut();
            $("#ListFlagDetailTable").fadeOut();
            $("#ListLocationDetailTable").fadeOut();
            $("#ListCategoryDetailTable").fadeOut();
        }
    });

    $(document).on('click', '.VRMakeInactive', function () {
        //alert($(this).attr('id'));
        if ($(this).attr('id') != "") {
            UpdateVacateReasons("Inactive",$(this).attr('id'));
            $("#ListVacateDetailTable").fadeIn();
            $("#ListOutcomeDetailTable").fadeOut();
            $("#ListFlagDetailTable").fadeOut();
            $("#ListLocationDetailTable").fadeOut();
            $("#ListCategoryDetailTable").fadeOut();
        }
    });

    $(document).on('click', '.VRMakeActive', function () {
        //alert($(this).attr('id'));
        if ($(this).attr('id') != "") {
            UpdateVacateReasons("Active",$(this).attr('id'));
            $("#ListVacateDetailTable").fadeIn();
            $("#ListOutcomeDetailTable").fadeOut();
            $("#ListFlagDetailTable").fadeOut();
            $("#ListLocationDetailTable").fadeOut();
            $("#ListCategoryDetailTable").fadeOut();
        }
    });

    $(document).on('click', '#VRFilter', function () {
        //alert($(this).attr('value'));
        if ($(this).attr('id') != "") {
            VRFilter = $(this).attr('value')
            getListVacateReasonDetails();
            $("#ListVacateDetailTable").fadeIn();
            $("#ListOutcomeDetailTable").fadeOut();
            $("#ListFlagDetailTable").fadeOut();
            $("#ListLocationDetailTable").fadeOut();
            $("#ListCategoryDetailTable").fadeOut();
        }
    });

    function UpdateVacateReasons(uType,vrid) {

        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/DiaryManagement/UpdateVacateReasons";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/UpdateVacateReasons";
        }

        //var urlMap = document.getElementById('V_VR_SelectName').value;
        var selectedVR = document.getElementById('V_VR_SelectName').value;

        $.ajax({
            url: pathURL,
            type: 'Get',
            data: { listId: selectedListId, VRName: selectedVR, diaryName: selectedDiaryName, updateType: uType, VRId: vrid },
            success: function (data) {
                $("#ListVacateDetailTable").empty().append(data);
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Unable to save vacate reason duplicate name exists, Please review.");
            }
        });

    }


    $(document).on('click', '#OutcomeSelected', function () {
        getListOutcomeDetails();
        $("#ListVacateDetailTable").fadeOut();
        $("#ListFlagDetailTable").fadeOut();
        $("#ListLocationDetailTable").fadeOut();
        $("#ListCategoryDetailTable").fadeOut();
        $("#ListOutcomeDetailTable").fadeIn();
        document.getElementById("LocationSelected").style.backgroundColor = 'threedlightshadow';
        document.getElementById("CategorySelected").style.backgroundColor = 'threedlightshadow';
        document.getElementById("FlagSelected").style.backgroundColor = 'threedlightshadow';
        document.getElementById("VacateReasonSelected").style.backgroundColor = 'threedlightshadow';
        document.getElementById("OutcomeSelected").style.backgroundColor = '#FFFF00';
    });

    function getListOutcomeDetails() {

        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/DiaryManagement/GetListOutcomeDetails";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/GetListOutcomeDetails";
        }

        $.ajax({
            // Get List Outcome Details PartialView
            url: pathURL,
            type: 'Get',
            data: { listId: selectedListId, diaryName: selectedDiaryName, oFilter: OFilter },
            success: function (data) {
                $("#ListOutcomeDetailTable").empty().append(data);
                $('.color').colorpicker();
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Error loading outcome details, please retry selection.");
            }

        });

    }

    $(document).on('change', '#V_O_SelectName', function () {
        if (document.getElementById("V_O_SelectName").value != "Select") {
            //alert("clicked");
            UpdateOutcomes("Add", "");
            $("#ListOutcomeDetailTable").fadeIn();
            $("#ListFlagDetailTable").fadeOut();
            $("#ListLocationDetailTable").fadeOut();
            $("#ListCategoryDetailTable").fadeOut();
            $("#ListVacateDetailTable").fadeOut();
        }
    });

    $(document).on('click', '.OMakeInactive', function () {
        //alert($(this).attr('id'));
        if ($(this).attr('id') != "") {
            UpdateOutcomes("Inactive", $(this).attr('id'));
            $("#ListOutcomeDetailTable").fadeIn();
            $("#ListFlagDetailTable").fadeOut();
            $("#ListLocationDetailTable").fadeOut();
            $("#ListCategoryDetailTable").fadeOut();
            $("#ListVacateDetailTable").fadeOut();
        }
    });

    $(document).on('click', '.OMakeActive', function () {
        //alert($(this).attr('id'));
        if ($(this).attr('id') != "") {
            UpdateOutcomes("Active", $(this).attr('id'));
            $("#ListOutcomeDetailTable").fadeIn();
            $("#ListFlagDetailTable").fadeOut();
            $("#ListLocationDetailTable").fadeOut();
            $("#ListCategoryDetailTable").fadeOut();
            $("#ListVacateDetailTable").fadeOut();
        }
    });

    $(document).on('click', '#OFilter', function () {
        //alert($(this).attr('value'));
        if ($(this).attr('id') != "") {
            OFilter = $(this).attr('value')
            getListOutcomeDetails();
            $("#ListOutcomeDetailTable").fadeIn();
            $("#ListFlagDetailTable").fadeOut();
            $("#ListLocationDetailTable").fadeOut();
            $("#ListCategoryDetailTable").fadeOut();
            $("#ListVacateDetailTable").fadeOut();
        }
    });

    function UpdateOutcomes(uType, oid) {

        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/DiaryManagement/UpdateOutcomes";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/UpdateOutcomes";
        }

        var selectedO = document.getElementById('V_O_SelectName').value;

        $.ajax({
            url: pathURL,
            type: 'Get',
            data: { listId: selectedListId, OName: selectedO, OType: OFilter, diaryName: selectedDiaryName, updateType: uType, OId: oid },
            success: function (data) {
                $("#ListOutcomeDetailTable").empty().append(data);
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Unable to save outcome duplicate name exists, Please review.");
            }
        });

    }

    $(document).on('click', '#FlagSelected', function () {
        //alert("Flag");
        getListFlagDetails();
        $("#ListOutcomeDetailTable").fadeOut();
        $("#ListFlagDetailTable").fadeIn();
        $("#ListLocationDetailTable").fadeOut();
        $("#ListCategoryDetailTable").fadeOut();
        $("#ListVacateDetailTable").fadeOut();
        document.getElementById("LocationSelected").style.backgroundColor = 'threedlightshadow';
        document.getElementById("CategorySelected").style.backgroundColor = 'threedlightshadow';
        document.getElementById("FlagSelected").style.backgroundColor = '#FFFF00';
        document.getElementById("VacateReasonSelected").style.backgroundColor = 'threedlightshadow';
        document.getElementById("OutcomeSelected").style.backgroundColor = 'threedlightshadow';
    });

    ////
    function getListFlagDetails() {

        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/DiaryManagement/GetListFlagDetails";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/GetListFlagDetails";
        }

        $.ajax({
            // Get List Flag Details PartialView
            url: pathURL,
            type: 'Get',
            data: { listId: selectedListId, diaryName: selectedDiaryName },
            success: function (data) {
                $("#ListFlagDetailTable").empty().append(data);
                $('.color').colorpicker();
                $("#FText").focus();
            },
            error: function (xhr, textStatus, errorThrown) {
                //alert(xhr.responseText);
                alert("Error loading flag details, please retry selection.");
            }

        });

    }

    $(document).on('click', '#AddFlag', function () {
        if (document.getElementById("FText").value != "") {
            if ($(this).attr('value') == "Add Flag") {
                UpdateFlags("Add", $("#V_F_Text").val());
                $("#AddFlag").val("Add Flag");
            }
            else
            {
                //UpdateFlags("Edit", document.getElementById("FText").value);;
                UpdateFlags("Edit", fid);;
            }
            $("#ListFlagDetailTable").fadeIn();
            $("#ListVacateDetailTable").fadeOut();
            $("#ListOutcomeDetailTable").fadeOut();
            $("#ListLocationDetailTable").fadeOut();
            $("#ListCategoryDetailTable").fadeOut();
        }
    });

    $(document).on('click', '.FMakeInactive', function () {
        //alert($(this).attr('id'));
        if ($(this).attr('id') != "") {
            UpdateFlags("Inactive", $(this).attr('id'));
            $("#ListFlagDetailTable").fadeIn();
            $("#ListVacateDetailTable").fadeOut();
            $("#ListOutcomeDetailTable").fadeOut();
            $("#ListLocationDetailTable").fadeOut();
            $("#ListCategoryDetailTable").fadeOut();
        }
    });

    $(document).on('click', '.FMakeActive', function () {
        //alert($(this).attr('id'));
        if ($(this).attr('id') != "") {
            UpdateFlags("Active", $(this).attr('id'));
            $("#ListFlagDetailTable").fadeIn();
            $("#ListVacateDetailTable").fadeOut();
            $("#ListOutcomeDetailTable").fadeOut();
            $("#ListLocationDetailTable").fadeOut();
            $("#ListCategoryDetailTable").fadeOut();
        }
    });

    $(document).on('click', '.FUpdate', function () {
        //var arrflag = $(this).attr('id').split('|')
        if ($(this).attr('id') != "") {
            var ftext = $(this).attr('title');
            fid = $(this).attr('id');
            $("#FText").val(ftext);
            $("#AddFlag").val("Update");
            $("#FText").focus();

        }
    });

    function UpdateFlags(uType, fid) {

        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/DiaryManagement/UpdateFlags";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/UpdateFlags";
        }

        var selectedF = document.getElementById('FText').value;

        $.ajax({
            url: pathURL,
            type: 'Get',
            data: { listId: selectedListId, FName: selectedF, diaryName: selectedDiaryName, updateType: uType, FId: fid },
            success: function (data) {
                $("#ListFlagDetailTable").empty().append(data);
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Unable to save flag duplicate name exists, Please review.");
            }
        });

    }

    ////

    $(document).on('click', '#LocationSelected', function () {
        //alert("Location");
        getListLocationDetails();
        $("#ListOutcomeDetailTable").fadeOut();
        $("#ListFlagDetailTable").fadeOut();
        $("#ListLocationDetailTable").fadeIn();
        $("#ListCategoryDetailTable").fadeOut();
        $("#ListVacateDetailTable").fadeOut();
        document.getElementById("LocationSelected").style.backgroundColor = '#FFFF00';
        document.getElementById("CategorySelected").style.backgroundColor = 'threedlightshadow';
        document.getElementById("FlagSelected").style.backgroundColor = 'threedlightshadow';
        document.getElementById("VacateReasonSelected").style.backgroundColor = 'threedlightshadow';
        document.getElementById("OutcomeSelected").style.backgroundColor = 'threedlightshadow';
    });

    function getListLocationDetails() {

        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/DiaryManagement/GetListLocationDetails";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/GetListLocationDetails";
        }

        $.ajax({
            // Get List Location Details PartialView
            url: pathURL,
            type: 'Get',
            data: { listId: selectedListId, diaryName: selectedDiaryName },
            success: function (data) {
                $("#ListLocationDetailTable").empty().append(data);
                $('.color').colorpicker();
                $("#LText").focus();
            },
            error: function (xhr, textStatus, errorThrown) {
                //alert(xhr.responseText);
                alert("Error loading location details, please retry selection.");
            }

        });

    }

    $(document).on('click', '#AddLocation', function () {
        if (document.getElementById("LText").value != "") {
            if ($(this).attr('value') == "Add Location") {
                UpdateLocations("Add", $("#V_L_Text").val());
                $("#UpdateLocations").val("Add Location");
            }
            else {
                UpdateLocations("Edit", lid);;
            }
            $("#ListOutcomeDetailTable").fadeOut();
            $("#ListFlagDetailTable").fadeOut();
            $("#ListLocationDetailTable").fadeIn();
            $("#ListCategoryDetailTable").fadeOut();
            $("#ListVacateDetailTable").fadeOut();
        }
    });

    $(document).on('click', '.LMakeInactive', function () {
        //alert($(this).attr('id'));
        if ($(this).attr('id') != "") {
            UpdateLocations("Inactive", $(this).attr('id'));
            $("#ListOutcomeDetailTable").fadeOut();
            $("#ListFlagDetailTable").fadeOut();
            $("#ListLocationDetailTable").fadeIn();
            $("#ListCategoryDetailTable").fadeOut();
            $("#ListVacateDetailTable").fadeOut();
        }
    });

    $(document).on('click', '.LMakeActive', function () {
        //alert($(this).attr('id'));
        if ($(this).attr('id') != "") {
            UpdateLocations("Active", $(this).attr('id'));
            $("#ListOutcomeDetailTable").fadeOut();
            $("#ListFlagDetailTable").fadeOut();
            $("#ListLocationDetailTable").fadeIn();
            $("#ListCategoryDetailTable").fadeOut();
            $("#ListVacateDetailTable").fadeOut();
        }
    });

    $(document).on('click', '.LUpdate', function () {
        if ($(this).attr('id') != "") {
            var ltext = $(this).attr('title');
            lid = $(this).attr('id');
            $("#LText").val(ltext);
            $("#AddLocation").val("Update");
            $("#LText").focus();

        }
    });

    function UpdateLocations(uType, lid) {

        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/DiaryManagement/UpdateLocations";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/UpdateLocations";
        }

        var selectedL = document.getElementById('LText').value;

        $.ajax({
            url: pathURL,
            type: 'Get',
            data: { listId: selectedListId, LName: selectedL, diaryName: selectedDiaryName, updateType: uType, LId: lid },
            success: function (data) {
                $("#ListLocationDetailTable").empty().append(data);
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Unable to save location duplicate name exists, Please review.");
            }
        });

    }

    ////

    $(document).on('click', '#CategorySelected', function () {
        //alert("Location");
        getListCategoryDetails();
        document.getElementById("LocationSelected").style.backgroundColor = 'threedlightshadow';
        document.getElementById("CategorySelected").style.backgroundColor = '#FFFF00';
        document.getElementById("FlagSelected").style.backgroundColor = 'threedlightshadow';
        document.getElementById("VacateReasonSelected").style.backgroundColor = 'threedlightshadow';
        document.getElementById("OutcomeSelected").style.backgroundColor = 'threedlightshadow';
        $("#ListCategoryDetailTable").fadeIn();
        $("#ListOutcomeDetailTabl   e").fadeOut();
        $("#ListFlagDetailTable").fadeOut();
        $("#ListLocationDetailTable").fadeOut();
        $("#ListVacateDetailTable").fadeOut();
    });

    function getListCategoryDetails() {

        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/DiaryManagement/GetListCategoryDetails";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/GetListCategoryDetails";
        }

        $.ajax({
            // Get List Category Details PartialView
            url: pathURL,
            type: 'Get',
            data: { listId: selectedListId, diaryName: selectedDiaryName },
            success: function (data) {
                $("#ListCategoryDetailTable").empty().append(data);
                $('.color').colorpicker();
                $("#CText").focus();
            },
            error: function (xhr, textStatus, errorThrown) {
                //alert(xhr.responseText);
                alert("Error loading category details, please retry selection.");
            }

        });

    }

    $(document).on('click', '#AddCategory', function () {

            if ($(this).attr('value') == "Add Category") {
                UpdateCategories("Add", $("#V_C_Text").val());
                $("#AddCategory").val("Add Category");
            }
            else {
                UpdateCategories("Edit", cid);;
            }

            //alert("clicked");
            $("#ListCategoryDetailTable").fadeIn();
            $("#ListOutcomeDetailTable").fadeOut();
            $("#ListFlagDetailTable").fadeOut();
            $("#ListLocationDetailTable").fadeOut();
            $("#ListVacateDetailTable").fadeOut();
    });

    $(document).on('click', '.CMakeInactive', function () {
        //alert($(this).attr('id'));
        if ($(this).attr('id') != "") {
            UpdateCategories("Inactive", $(this).attr('id'));
            $("#ListCategoryDetailTable").fadeIn();
            $("#ListOutcomeDetailTable").fadeOut();
            $("#ListFlagDetailTable").fadeOut();
            $("#ListLocationDetailTable").fadeOut();
            $("#ListVacateDetailTable").fadeOut();
        }
    });

    $(document).on('click', '.CMakeActive', function () {
        //alert($(this).attr('id'));CUpdate
        if ($(this).attr('id') != "") {
            UpdateCategories("Active", $(this).attr('id'));
            $("#ListCategoryDetailTable").fadeIn();
            $("#ListOutcomeDetailTable").fadeOut();
            $("#ListFlagDetailTable").fadeOut();
            $("#ListLocationDetailTable").fadeOut();
            $("#ListVacateDetailTable").fadeOut();
        }
    });

    $(document).on('click', '.CUpdate', function () {
        if ($(this).attr('id') != "") {
            var ctext = $(this).attr('title');
            cid = $(this).attr('id');
            $("#CText").val(ctext);
            $("#AddCategory").val("Update");
            $("#CText").focus();

        }
    });
    function UpdateCategories(uType, cid) {

        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/DiaryManagement/UpdateCategories";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/UpdateCategories";
        }

        var selectedC = document.getElementById('CText').value;

        $.ajax({
            url: pathURL,
            type: 'Get',
            data: { listId: selectedListId, CName: selectedC, diaryName: selectedDiaryName, updateType: uType, CId: cid },
            success: function (data) {
                $("#ListCategoryDetailTable").empty().append(data);
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Unable to save category duplicate name exists, Please review.");
            }
        });

    }


    

    $(document).on('click', '.ReinstateList', function () {
        //alert('ReinstateList');
        if ($(this).attr('id') != "") {
            UpdateList("Reinstate", $(this).attr('id'));
        }
    });

    $(document).on('click', '.DeleteList', function () {
        //alert('DeleteList');
        if ($(this).attr('id') != "") {
            UpdateList("Delete", $(this).attr('id'));
        }
    });

    function UpdateList(uType, lid) {

        pathArray = window.location.href.split('/');
        if (window.location.hostname.indexOf("localhost") >= 0) {
            pathURL = "/DiaryManagement/UpdateList";
        }
        else {
            pathURL = "/" + pathArray[3] + "/" + pathArray[4] + "/DiaryManagement/UpdateList";
        }

        $.ajax({
            url: pathURL,
            type: 'Get',
            data: { updateType: uType, listId: lid },
            success: function (data) {
                location.reload(true);
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("1 Error updating list, please retry.");
            }
        });

    }

    function handleSuccess(content) {
            $("#name-input").html(content);  
            $("#name-input").addClass("easy-notification");  
        }  

    $(".ListTypeDetailsTable").click(function () {
        alert($(this).attr('id'));
        alert("diarylistings = " + $(this).find(".ListingsMng").val());

    });

});
