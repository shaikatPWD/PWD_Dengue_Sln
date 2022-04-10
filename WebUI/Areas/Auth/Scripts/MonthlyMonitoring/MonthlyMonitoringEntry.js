$(document).ready(function () {
    var MonthlyMonitoringInfoViewModel = function () {
        var self = this;
        self.Id = ko.observable();
        self.AssetList = ko.observableArray([]);

        self.Url = ko.observable();

        self.LoadAssets = function () {
            return $.ajax({
                type: "GET",
                url: '/Auth/MonthlyMonitoring/LoadMonthlyMonitorinInfoByOffice',
                contentType: "application/json; charset=utf-8",
                dataType: "json",                
                success: function (data) {
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