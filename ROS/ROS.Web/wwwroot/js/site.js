// Write your JavaScript code.
$(document).on('click', '.panel-heading span.clickable', function(e){
    var $this = $(this);
	if(!$this.hasClass('panel-collapsed')) {
		$this.parents('.panel').find('.specialCollapse').slideUp();
		$this.addClass('panel-collapsed');
		$this.find('i').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
	} else {
		$this.parents('.panel').find('.specialCollapse').slideDown();
		$this.removeClass('panel-collapsed');
		$this.find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
	}
})

var maxDescLength = 1000;
$('#desctextChars').keyup(function () {
    var length = $(this).val().length;
    var length = maxDescLength - length;
    $('#descChars').text("(" + length + " tecken kvar)");
});

var maxTitleLength = 50;
$('#titletextChars').keyup(function () {
    var length = $(this).val().length;
    var length = maxTitleLength - length;
    $('#titleChars').text("(" + length + " tecken kvar)");
});