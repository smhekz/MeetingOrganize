var ViewModel = function () {

    var self = this;
    self.meetings = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.participants = ko.observableArray();
    
    self.newMeeting = {
        Participant: ko.observable(),
        Subject: ko.observable(),
        Date: ko.observable(),
        StartTime: ko.observable(),
        FinishTime: ko.observable()
    }

    var meetingsUri = '/api/meetings/';
    var participantsUri = '/api/participants/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllMeetings() {
        ajaxHelper(meetingsUri, 'GET').done(function (data) {
            self.meetings(data);
        });
    }

    self.getMeetingDetail = function (item) {
        ajaxHelper(meetingsUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    //self.getMeetingDetail = function (item) {
    //    ajaxHelper(meetingsUri + item.Id, 'GET').done(function (data) {
    //        self.
            
    //    });
    //}

    function getParticipants() {
        ajaxHelper(participantsUri, 'GET').done(function (data) {
            self.participants(data);
        });
    }
    self.deleteMeeting = function (item) {
        //self.meetings.remove(item);
        $.ajax({
            type: "DELETE",
            url: meetingsUri + item.Id
        }).done(function (item) {
            self.meetings.remove(item);
            location.reload();
        });

    }

    self.addMeeting = function (formElement) {
        var meeting = {
            ParticipantId: self.newMeeting.Participant().Id,
            Subject: self.newMeeting.Subject(),
            Date: self.newMeeting.Date(),
            StartTime: self.newMeeting.StartTime(),
            FinishTime: self.newMeeting.FinishTime()
        };

        ajaxHelper(meetingsUri, 'POST', meeting).done(function (item) {
            self.meetings.push(item);
        });
    }

    // Fetch the initial data.
    getAllMeetings();
    getParticipants();
};

ko.applyBindings(new ViewModel());