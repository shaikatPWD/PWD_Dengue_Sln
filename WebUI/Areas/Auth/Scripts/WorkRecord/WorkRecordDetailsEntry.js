$(document).ready(function () {
    var WorkRecordDetailsViewModel = function () {
        var self = this;
        self.Id = ko.observable();
        self.AssetId = ko.observable();
        self.AssetName = ko.observable();
        self.AssetBuildingName = ko.observable();

        self.CompletionDate = ko.observable(moment());
        self.CompletionDateText = ko.observable('');

        self.Image1 = ko.observable();
        self.Image2 = ko.observable();
        //console.log(self.Image1());
        self.LoadAssetNameAssetId = function () {
            //if (self.AssetId() > 0) {
                return $.ajax({
                    type: "GET",
                    url: "/Auth/Workrecord/GetAssetNamebyAssetId?assetId=" + self.AssetId(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        self.AssetId(data.Id);
                        //self.AssetId(data.AssetId);
                        self.AssetName(data.AssetTypeFull);
                        //self.AssetBuildingName(data.AssetBuildingName);
                        //self.CompletionDate(moment(data.CompletionDate));

                        //if (data.Image1 != null && data.Image1 != "") {
                        //    self.Image1(data.Image1);
                        //}
                        //else {
                        //    self.Image1("/UploadImages/ComplainImages/noimage.png");
                        //}
                        //if (data.Image2 != null && data.Image2 != "") {
                        //    self.Image2(data.Image2);
                        //}
                        //else {
                        //    self.Image2("/UploadImages/ComplainImages/noimage.png");
                        //}                        
                    },
                    error: function (error) {
                        alert(error.status + "<--and--> " + error.statusText);
                    }
                });
            //}
        }

        
        //console.log(self.AssetName());
        self.Submit = function () {
            self.CompletionDateText(moment(self.CompletionDate()).format('DD/MM/YYYY HH:mm'));
            var workRecordDetailsData = {
                Id: self.Id(),
                AssetId: self.AssetId(),
                AssetName: self.AssetName(),
                AssetBuildingName: self.AssetBuildingName(),
                CompletionDate: self.CompletionDate(),
                CompletionDateText: self.CompletionDateText(),
                Image1: img1,
                Image2: img2
            };
            console.log(ko.toJSON(workRecordDetailsData));
            $.ajax({
                url: '/Auth/Workrecord/SaveWorkRecord',
                type: 'POST',
                contentType: 'application/json',
                data: ko.toJSON(workRecordDetailsData),
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

    var infoVm = new WorkRecordDetailsViewModel();
    
    infoVm.AssetId(infoVm.queryString("assetId"));
    infoVm.LoadAssetNameAssetId();
    //infoVm.LoadWorkRecordsByAsset();

    ko.applyBindings(infoVm, document.getElementById("wrdEntry")[0]);
})