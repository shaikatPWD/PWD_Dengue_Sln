$(document).ready(function () {

    var ActivityDetailsViewModel = function () {
        var self = this;
        self.Id = ko.observable();
        self.OfficeAssetId = ko.observable();
        self.Period = ko.observable();
        self.AssetList = ko.observableArray([]);
        self.LoadAssets = function () {
            return $.ajax({
                type: "GET",
                url: "/Auth/Activity/LoadWorkActivityByInstallationIdPeriod?officeassetId=" + self.OfficeAssetId() + "&period=" + self.Period(),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    self.AssetList(data);
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

    var infoVm = new ActivityDetailsViewModel();
    infoVm.OfficeAssetId(infoVm.queryString("officeassetId"));
    infoVm.Period(infoVm.queryString("period"));
    infoVm.LoadAssets();
    ko.applyBindings(infoVm, document.getElementById("mmInfoEntry")[0]);
})