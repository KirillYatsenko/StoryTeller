

var viewsHub = $.connection.viewsHub;
var likesHub = $.connection.likesHub;
var votingHub = $.connection.votingHub;

$.connection.hub.start().done(function () {

    $(document).on('click', '.title', function () {
        var storyID = $(this).attr("dataStoryID");
        viewsHub.server.read(storyID);
    });

})

viewsHub.client.updateViewsCount = function (viewsCount, storyID) {
    $("#viewsCount" + storyID).text(viewsCount);
}

likesHub.client.updateLikeCount = function (likeCount, storyID) {
    $("#likeCount" + storyID).text(likeCount);
}

votingHub.client.addChapterToVote = function (chapterToVote) {
    $("#chaptersToVote").append(chapterToVote);
}

