
$(document).ready(function () {
    //function ActivityDetailsLine() {
    //    var table = $('table');
    //    var activity = [];

    //    table.find('tr').each(function (i, el) {
    //        // no thead
    //        if (i != 0) {
    //            var $tds = $(this).find('td');
    //            var row = [];
    //            $tds.each(function (i, el) {
    //                row.push($(this).text());
    //            });
    //            activity.push(row);
    //        }

    //    });
    //    return activity;
    //}
    //console.log(toJSON(ActivityDetailsLine()));
    var activityDetailsLine = function () {
        var self = this;
        self.Id = ko.observable();
        self.AssetName = ko.observable();
        self.Date = ko.observable(moment());
        self.DateText = ko.observable('');
        //self.Period = ko.observable();
        //self.OfficeAssetId = ko.observable();
        

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

        self.ActivityDetailsData = function (data) {
            self.Id(data.Id);
            self.Date(data.Date);
            self.DateText(data.DateText);
            self.AssetName(data.AssetName);

            $.when(self.LoadIsPondsCleanUp()).done(function () {
                self.IsPondsCleanUp(data.IsPondsCleanUp);
                self.IsPondsCleanUpName(data.IsPondsCleanUpName);
            });

            $.when(self.LoadIsWastageCleanUp()).done(function () {
                self.IsWastageCleanUp(data.IsWastageCleanUp);
                self.IsWastageCleanUpName(data.IsWastageCleanUpName);
            });

            $.when(self.LoadIsMedicalCollegeCleanUp()).done(function () {
                self.IsMedicalCollegeCleanUp(data.IsMedicalCollegeCleanUp);
                self.IsMedicalCollegeCleanUpName(data.IsMedicalCollegeCleanUpName);
            });

            $.when(self.LoadIsOfficeAndHouseholdCleanUp()).done(function () {
                self.IsOfficeAndHouseholdCleanUp(data.IsOfficeAndHouseholdCleanUp);
                self.IsOfficeAndHouseholdCleanUpName(data.IsOfficeAndHouseholdCleanUpName);
            });

            $.when(self.LoadIsStillWaterCleanUp()).done(function () {
                self.IsStillWaterCleanUp(data.IsStillWaterCleanUp);
                self.IsStillWaterCleanUpName(data.IsStillWaterCleanUpName);
            });

            $.when(self.LoadIsCuringWaterCleanUp()).done(function () {
                self.IsCuringWaterCleanUp(data.IsCuringWaterCleanUp);
                self.IsCuringWaterCleanUpName(data.IsCuringWaterCleanUpName);
            });

            $.when(self.LoadIsUnderConstructionBuildingCleanUp()).done(function () {
                self.IsUnderConstructionBuildingCleanUp(data.IsUnderConstructionBuildingCleanUp);
                self.IsUnderConstructionBuildingCleanUpName(data.IsUnderConstructionBuildingCleanUpName);
            });
        };
    };

    var ActivitiesDetailsEntryViewModel = function () {
        var self = this;
        //self.Id = ko.observable();
        //self.Date = ko.observable(moment());
        //self.DateText = ko.observable('');
        //self.Period = ko.observable();
        //self.OfficeAssetId = ko.observable();
        //self.OfficeAssetName = ko.observable();

        //self.AssetList = ko.observableArray([]);

        //self.IsPondsCleanUp = ko.observable('');// Blocked or Not used
        //self.IsPondsCleanUpName = ko.observable('');
        //self.IsPondsCleanUpList = ko.observableArray([]);

        //self.IsWastageCleanUp = ko.observable('');// Blocked or Not used
        //self.IsWastageCleanUpName = ko.observable('');
        //self.IsWastageCleanUpList = ko.observableArray([]);

        //self.IsMedicalCollegeCleanUp = ko.observable('');// Blocked or Not used
        //self.IsMedicalCollegeCleanUpName = ko.observable('');
        //self.IsMedicalCollegeCleanUpList = ko.observableArray([]);

        //self.IsOfficeAndHouseholdCleanUp = ko.observable('');// Blocked or Not used
        //self.IsOfficeAndHouseholdCleanUpName = ko.observable('');
        //self.IsOfficeAndHouseholdCleanUpList = ko.observableArray([]);

        //self.IsStillWaterCleanUp = ko.observable('');// Blocked or Not used
        //self.IsStillWaterCleanUpName = ko.observable('');
        //self.IsStillWaterCleanUpList = ko.observableArray([]);

        //self.IsCuringWaterCleanUp = ko.observable('');// Blocked or Not used
        //self.IsCuringWaterCleanUpName = ko.observable('');
        //self.IsCuringWaterCleanUpList = ko.observableArray([]);

        //self.IsUnderConstructionBuildingCleanUp = ko.observable('');// Blocked or Not used
        //self.IsUnderConstructionBuildingCleanUpName = ko.observable('');
        //self.IsUnderConstructionBuildingCleanUpList = ko.observableArray([]);new ActivityDetailsLine()

        self.ActivityDetails = ko.observableArray([]);
        //self.ActivityDetailsEnData = ko.observableArray([new ActivityDetailsLine()]);

        //self.LoadIsPondsCleanUp = function () {
        //    return $.ajax({
        //        type: "GET",
        //        url: '/Auth/Activity/GetIsComplete',
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (data) {
        //            self.IsPondsCleanUpList(data);
        //        },
        //        error: function (error) {
        //            alert(error.status + "<--and--> " + error.statusText);
        //        }
        //    });
        //}

        //self.LoadIsWastageCleanUp = function () {
        //    return $.ajax({
        //        type: "GET",
        //        url: '/Auth/Activity/GetIsComplete',
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (data) {
        //            self.IsWastageCleanUpList(data);
        //        },
        //        error: function (error) {
        //            alert(error.status + "<--and--> " + error.statusText);
        //        }
        //    });
        //}

        //self.LoadIsMedicalCollegeCleanUp = function () {
        //    return $.ajax({
        //        type: "GET",
        //        url: '/Auth/Activity/GetIsComplete',
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (data) {
        //            self.IsMedicalCollegeCleanUpList(data);
        //        },
        //        error: function (error) {
        //            alert(error.status + "<--and--> " + error.statusText);
        //        }
        //    });
        //}

        //self.LoadIsOfficeAndHouseholdCleanUp = function () {
        //    return $.ajax({
        //        type: "GET",
        //        url: '/Auth/Activity/GetIsComplete',
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (data) {
        //            self.IsOfficeAndHouseholdCleanUpList(data);
        //        },
        //        error: function (error) {
        //            alert(error.status + "<--and--> " + error.statusText);
        //        }
        //    });
        //}

        //self.LoadIsStillWaterCleanUp = function () {
        //    return $.ajax({
        //        type: "GET",
        //        url: '/Auth/Activity/GetIsComplete',
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (data) {
        //            self.IsStillWaterCleanUpList(data);
        //        },
        //        error: function (error) {
        //            alert(error.status + "<--and--> " + error.statusText);
        //        }
        //    });
        //}

        //self.LoadIsCuringWaterCleanUp = function () {
        //    return $.ajax({
        //        type: "GET",
        //        url: '/Auth/Activity/GetIsComplete',
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (data) {
        //            self.IsCuringWaterCleanUpList(data);
        //        },
        //        error: function (error) {
        //            alert(error.status + "<--and--> " + error.statusText);
        //        }
        //    });
        //}

        //self.LoadIsUnderConstructionBuildingCleanUp = function () {
        //    return $.ajax({
        //        type: "GET",
        //        url: '/Auth/Activity/GetIsComplete',
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (data) {
        //            self.IsUnderConstructionBuildingCleanUpList(data);
        //        },
        //        error: function (error) {
        //            alert(error.status + "<--and--> " + error.statusText);
        //        }
        //    });
        //}

        self.LoadActivitiesByPeriod = function () {
            return $.ajax({
                type: "GET",
                url: "/Auth/ActivityHome/GetWorkActivityRecordsByPeriod",//?period=" + self.period(),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.length > 0) {
                        $.each(data, function (index, value) {
                            var activityDetails = new activityDetailsLine();
                            if (typeof (value) != 'undefined') {
                                activityDetails.ActivityDetailsData(value);
                                self.ActivityDetails.push(activityDetails);
                            }
                        });
                    }

                    //console.log(self.ActivityDetails);
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }

        console.log(self.ActivityDetails());

        self.Submit = function () {            
            $.ajax({
                url: '/Auth/ActivityHome/SaveWorkActivityDetails',
                type: 'POST',
                contentType: 'application/json',
                data: ko.toJSON(self.ActivityDetails()),
                success: function (data) {
                    $('#successModal').modal('show');
                    $('#successModalText').text(data.Message);                    
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

    var infoVm = new ActivitiesDetailsEntryViewModel();
    //var activDet = new ActivityDetailsLine();
    //infoVm.OfficeAssetId(infoVm.queryString("officeassetId"));
    //activDet.Period(infoVm.queryString("period"));
    infoVm.LoadActivitiesByPeriod();
    
    //infoVm.LoadIsPondsCleanUp();
    //infoVm.LoadIsWastageCleanUp();
    //infoVm.LoadIsMedicalCollegeCleanUp();
    //infoVm.LoadIsOfficeAndHouseholdCleanUp();
    //infoVm.LoadIsStillWaterCleanUp();
    //infoVm.LoadIsCuringWaterCleanUp();
    //infoVm.LoadIsUnderConstructionBuildingCleanUp();

    ko.applyBindings(infoVm, document.getElementById("activityDetails")[0]);
})