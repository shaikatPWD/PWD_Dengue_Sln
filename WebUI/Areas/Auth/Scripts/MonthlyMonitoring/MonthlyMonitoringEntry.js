$(document).ready(function () {

    var MonthlyMonitoringInfoLine = function () {
        var self = this;
        self.Id = ko.observable();
        self.OfficeId = ko.observable();
        self.Period = ko.observable();
        self.OfficeAssetId = ko.observable();
        self.OfficeAssetName = ko.observable();
        self.IsPondsCleanUp = ko.observable();
        self.IsPondCleanUpName = ko.observable();
        self.IsWastageCleanUp = ko.observable();
        self.IsWastageCleanUpName = ko.observable();
        self.IsMedicalCollegeCleanUp = ko.observable();
        self.IsMedicalCollegeCleanUpName = ko.observable();
        self.IsOfficeAndHouseholdCleanUp = ko.observable();
        self.IsOfficeAndHouseholdCleanUpName = ko.observable();
        self.IsStillWaterCleanUp = ko.observable();
        self.IsStillWaterCleanUpName = ko.observable();
        self.IsCuringWaterCleanUp = ko.observable();
        self.IsCuringWaterCleanUpName = ko.observable();
        self.IsUnderConstructionBuildingCleanUp = ko.observable();
        self.IsUnderConstructionBuildingCleanUpName = ko.observable();
        self.LoadMonthlyMonitoringInfoData = function (data) {
            self.Id(data.Id);
            self.OfficeId(data.OfficeId);
            self.Period(data.Period);
            self.OfficeAssetId(data.OfficeAssetId);
            self.OfficeAssetName(data.OfficeAssetName);
            self.IsPondsCleanUp(data.IsPondsCleanUp);
            self.IsPondCleanUpName(data.IsPondCleanUpName);
            self.IsWastageCleanUp(data.IsWastageCleanUp);
            self.IsWastageCleanUpName(data.IsWastageCleanUpName);
            self.IsMedicalCollegeCleanUp(data.IsMedicalCollegeCleanUp);
            self.IsMedicalCollegeCleanUpName(data.IsMedicalCollegeCleanUpName);
            self.IsOfficeAndHouseholdCleanUp(data.IsOfficeAndHouseholdCleanUp);
            self.IsOfficeAndHouseholdCleanUpName(data.IsOfficeAndHouseholdCleanUpName);
            self.IsStillWaterCleanUp(data.IsStillWaterCleanUp);
            self.IsStillWaterCleanUpName(data.IsStillWaterCleanUpName);
            self.IsCuringWaterCleanUp(data.IsCuringWaterCleanUp);
            self.IsCuringWaterCleanUpName(data.IsCuringWaterCleanUpName);            
        };
    };

    var MonthlyMonitoringInfoViewModel = function () {
        var self = this;
        self.Id = ko.observable();
        self.AssetList = ko.observableArray([]);

        self.Url = ko.observable();

        self.MonthlyMonitoringInfo = ko.observableArray([new MonthlyMonitoringInfoLine()]);

        self.LoadAssets = function () {
            return $.ajax({
                type: "GET",
                url: '/Auth/MonthlyMonitoring/LoadMonthlyMonitorinInfoByOffice',
                contentType: "application/json; charset=utf-8",
                dataType: "json",                
                success: function (data) {

                    //self.MonthlyMonitoringInfo([]);

                    //if (data.length > 0) {
                    //    $.each(data, function (index, value) {
                    //        var mmInfo = new MonthlyMonitoringInfoLine();
                    //        if (typeof (value) != 'undefined') {
                    //            mmInfo.LoadMonthlyMonitoringInfoData(value);
                    //            self.MonthlyMonitoringInfo.push(mmInfo);
                    //        }
                    //    });
                    //}

                    self.AssetList(data);
                    //sel.Url(data.WorkRecordDetails.Image1);
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }                   

        self.queryString = function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        };
    };

    var infoVm = new MonthlyMonitoringInfoViewModel();
    infoVm.LoadAssets();
    ko.applyBindings(infoVm, document.getElementById("mmInfoEntry")[0]);
})