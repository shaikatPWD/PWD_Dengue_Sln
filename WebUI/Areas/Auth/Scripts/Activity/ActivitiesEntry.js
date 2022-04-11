$(document).ready(function () {
    var ActivitiesViewModel = function () {
        var self = this;
        self.Id = ko.observable();
        self.Date = ko.observable(moment());
        self.DateText = ko.observable('');
        self.Period = ko.observable();
        self.OfficeAssetId = ko.observable();
        self.OfficeAssetName = ko.observable();

        self.IsPondsCleanUp = ko.observable('');// Blocked or Not used
        self.IsPondsCleanUpName = ko.observable('');
        self.IsPondsCleanUpList = ko.observableArray([]);


        self.IsWastageCleanUp = ko.observable('');// Blocked or Not used
        self.IsWastageCleanUpName = ko.observable('');
        self.IsWastageCleanUpList = ko.observableArray([]);


        self.IsMedicalCollegeCleanUp = ko.observable('');// Blocked or Not used
        self.IsMedicalCollegeCleanUpName = ko.observable('');
        self.IsMedicalCollegeCleanUpList = ko.observableArray([]);


        self.IsOfficeAndHouseholdCleanUp = ko.observable('');// Blocked or Not used
        self.IsOfficeAndHouseholdCleanUpName = ko.observable('');
        self.IsOfficeAndHouseholdCleanUpList = ko.observableArray([]);


        self.IsStillWaterCleanUp = ko.observable('');// Blocked or Not used
        self.IsStillWaterCleanUpName = ko.observable('');
        self.IsStillWaterCleanUpList = ko.observableArray([]);


        self.IsCuringWaterCleanUp = ko.observable('');// Blocked or Not used
        self.IsCuringWaterCleanUpName = ko.observable('');
        self.IsCuringWaterCleanUpList = ko.observableArray([]);


        self.IsUnderConstructionBuildingCleanUp = ko.observable('');// Blocked or Not used
        self.IsUnderConstructionBuildingCleanUpName = ko.observable('');
        self.IsUnderConstructionBuildingCleanUpList = ko.observableArray([]);


        self.LoadIsPondsCleanUp = function () {
            return $.ajax({
                type: "GET",
                url: '/Auth/Activity/GetIsComplete',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    self.IsPondsCleanUpList(data);
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }

        self.LoadIsWastageCleanUp = function () {
            return $.ajax({
                type: "GET",
                url: '/Auth/Activity/GetIsComplete',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    self.IsWastageCleanUpList(data);
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }

        self.LoadIsMedicalCollegeCleanUp = function () {
            return $.ajax({
                type: "GET",
                url: '/Auth/Activity/GetIsComplete',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    self.IsMedicalCollegeCleanUpList(data);
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }

        self.LoadIsOfficeAndHouseholdCleanUp = function () {
            return $.ajax({
                type: "GET",
                url: '/Auth/Activity/GetIsComplete',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    self.IsOfficeAndHouseholdCleanUpList(data);
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }

        self.LoadIsStillWaterCleanUp = function () {
            return $.ajax({
                type: "GET",
                url: '/Auth/Activity/GetIsComplete',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    self.IsStillWaterCleanUpList(data);
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }

        self.LoadIsCuringWaterCleanUp = function () {
            return $.ajax({
                type: "GET",
                url: '/Auth/Activity/GetIsComplete',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    self.IsCuringWaterCleanUpList(data);
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }

        self.LoadIsUnderConstructionBuildingCleanUp = function () {
            return $.ajax({
                type: "GET",
                url: '/Auth/Activity/GetIsComplete',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    self.IsUnderConstructionBuildingCleanUpList(data);
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }


        //console.log(self.Image1());
        self.LoadAssetNameAssetId = function () {
            //if (self.AssetId() > 0) {
                return $.ajax({
                    type: "GET",
                    url: "/Auth/Activity/GetOfficeAssetNamebyId?officeassetId=" + self.OfficeAssetId(),// + "&period=" + self.period(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        self.OfficeAssetId(data.Id);                        
                        self.OfficeAssetName(data.AssetName);                       
                    },
                    error: function (error) {
                        alert(error.status + "<--and--> " + error.statusText);
                    }
                });
            //}
        }

        
        //console.log(self.AssetName());
        self.Submit = function () {
            self.DateText(moment(self.Date()).format('DD/MM/YYYY HH:mm'));
            var activitiesData = {
                Id: self.Id(),
                Date: self.Date(),
                DateText: self.DateText(),
                Period: self.Period(),
                OfficeAssetId: self.OfficeAssetId(),
                OfficeAssetName: self.OfficeAssetName(),
                IsPondsCleanUp: self.IsPondsCleanUp(),
                IsWastageCleanUp: self.IsWastageCleanUp(),
                IsMedicalCollegeCleanUp: self.IsMedicalCollegeCleanUp(),
                IsOfficeAndHouseholdCleanUp: self.IsOfficeAndHouseholdCleanUp(),
                IsStillWaterCleanUp: self.IsStillWaterCleanUp(),
                IsCuringWaterCleanUp: self.IsCuringWaterCleanUp(),
                IsUnderConstructionBuildingCleanUp: self.IsUnderConstructionBuildingCleanUp(),
            };
            
            $.ajax({
                url: '/Auth/Activity/SaveWorkActivityforInstallation',
                type: 'POST',
                contentType: 'application/json',
                data: ko.toJSON(activitiesData),
                success: function (data) {
                    $('#successModal').modal('show');
                    $('#successModalText').text(data.Message);
                    if (data.Id > 0) {
                        //self.LoadWorkRecordsByAsset();
                    }
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        };

        self.queryString = function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        };
    };

    var infoVm = new ActivitiesViewModel();

    infoVm.OfficeAssetId(infoVm.queryString("officeassetId"));
    infoVm.Period(infoVm.queryString("period"));
    infoVm.LoadIsPondsCleanUp();
    infoVm.LoadIsWastageCleanUp();
    infoVm.LoadIsMedicalCollegeCleanUp();
    infoVm.LoadIsOfficeAndHouseholdCleanUp();
    infoVm.LoadIsStillWaterCleanUp();
    infoVm.LoadIsCuringWaterCleanUp();
    infoVm.LoadIsUnderConstructionBuildingCleanUp();
    infoVm.LoadAssetNameAssetId();

    ko.applyBindings(infoVm, document.getElementById("wrdEntry")[0]);
})