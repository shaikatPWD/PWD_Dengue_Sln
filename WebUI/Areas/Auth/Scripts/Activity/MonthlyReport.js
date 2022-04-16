$(document).ready(function () {

    var MReportViewModel = function () {
        var self = this;
        self.Id = ko.observable();
        self.PeriodList = ko.observableArray([]);
        self.Period = ko.observable();
        self.Url = ko.observable();

        self.LoadPeriods = function () {
            return $.ajax({
                type: "GET",
                url: '/Auth/ActivityHome/GetWorkActivitiesByPeriod?period=' + self.Period(),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    self.PeriodList(data);
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

    var infoVm = new MReportViewModel();
    infoVm.Period(infoVm.queryString("period"));
    infoVm.LoadPeriods();
    ko.applyBindings(infoVm, document.getElementById("mmInfoEntry")[0]);
})