//---Add Punjabi Keyboard---//
//---add keyboardInput class to input control---//
function AddPunjabiText(targetId) {
    // Load the Google Transliteration API
    google.load("elements", "1", {
        packages: "transliteration"
    });
    function onLoad() {

        var options = {

            sourceLanguage: 'en',

            destinationLanguage: ['pa', 'hi'],

            shortcutKey: 'ctrl+m',

            transliterationEnabled: true

        };

        // Create an instance on TransliterationControl with the required

        // options.

        var control = new google.elements.transliteration.TransliterationControl(options);

        // Enable transliteration in the textfields with the given ids.

        var ids = [];
        ids.push(targetId);

        control.makeTransliteratable(ids);

        // Show the transliteration control which can be used to toggle between

        // English and Hindi and also choose other destination language.

        control.showControl("translControl");
    }
    google.setOnLoadCallback(onLoad);
}

//---Disable Keyboard Input for Textbox---//
function DisableKeyboardInput(targetId) {
    $(targetId).keypress(function (event) {
        event.preventDefault();
        return false;
    });
}


//---Confirm Click Action---//
function confirmAction() {
        return confirm("You want to delete this");
}
//--- Confirm Subject Delete ---//
function ConfirmSubjectDelete(subjectId)
{
    var confirmDelete = confirm("You want to delete this");
    if (confirmDelete) {
        $.ajax({
            url: "/Subjects/Home/CheckIfSubjectHaveSubSubjects/" + subjectId,
            method: "POST",
            success: function (response) {
                if (response.statusCode == 1) {
                    alert("Subject is associated with sub-subjects.");
                    return false;
                }
                else {
                    window.location.href = '/Subjects/Home/Delete/'+subjectId;
                    return true;
                }
            }
        });
    }
        return false;
}
