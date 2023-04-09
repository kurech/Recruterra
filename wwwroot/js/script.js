function CreateResponse(iduser, idvacancy) {
    $.get(`/Home/AddResponse?iduser=${iduser}&idvacancy=${idvacancy}`);
};

function RedirectVacancyToOneOfTheVacancy(idvacancy, iduser) {
    window.location.href = '/Vacancys/OneOfTheVacancy/' + idvacancy + '/' + iduser;
};

function CreateMeeting(iduser, name, surname, descrip, dateandtime) {
    $.get(`/Home/AddMeet?iduser=${iduser}&name=${name}&surname=${surname}&descrip=${descrip}&dateandtime=${dateandtime}`);
};

function CreateVacancy(vacposition, vacobligations, vacsalary, vacworkex, vacdescrip, vacedu, vactypeofemp, vacisactive, vacidemployer) {
    $.get(`/Home/AddVac?vacposition=${vacposition}&vacobligations=${vacobligations}&vacsalary=${vacsalary}&vacworkex=${vacworkex}&vacdescrip=${vacdescrip}&vacedu=${vacedu}&vactypeofemp=${vactypeofemp}&vacisactive=${vacisactive}&idemployer=${vacidemployer}`);
};

function DeleteVacancy(idvacancy) {
    $.get(`/Home/DelVacancy?idvacancy=${idvacancy}`);
}

function DeleteMeeting(iduser, idmeeting) {
    $.get(`/Home/DelMeeting?iduser=${iduser}&idmeet=${idmeeting}`);
};

function UpdateAccount(id) {
    $.get(`/Home/UpadateAccountAfterLogout?idaccount=${id}`);
    window.location.href = '/Home/Index/';
};

function UpdateProfile(id, photo, password) {
    console.log(photo);
    console.log(password);
    $.get(`/Home/UpadateProfileSettings?iduser=${id}&photo=${photo}&password=${password}`);
};

function UpdateResume(id, photoresume, nameresume, surnameresume, positionresume,salaryresume,dateofresume,phoneresume,cityresume,citizenshipresume,eduresume,workresume,employresume,addinforesume) {
    $.get(`/Home/UpdateResumeSettings?iduser=${id}&photoresume=${photoresume}&nameresume=${nameresume}&surnameresume=${surnameresume}&positionresume=${positionresume}&salaryresume=${salaryresume}&dateofresume=${dateofresume}&phoneresume=${phoneresume}&cityresume=${cityresume}&citizenshipresume=${citizenshipresume}&eduresume=${eduresume}&workresume=${workresume}&employresume=${employresume}&addinforesume=${addinforesume}`);
    location.reload(), history.go(0), location.href = location.href, location.href = location.pathname, location.replace(location.pathname), location.reload(true)
};

function UpdateMeetingFilter(iduser) {
    if (document.getElementById('weekRadio').checked) {
        rate_value = document.getElementById('weekRadio').value;
    }
    $.get(`/Home/GetMeetings?iduser=${iduser}`);
    location.reload(), history.go(0), location.href = location.href, location.href = location.pathname, location.replace(location.pathname), location.reload(false)
};

function AcceptResponse(idresponse) {
    $.get(`/Home/AcceptResponse?idresponse=${idresponse}`);
    location.reload(), history.go(0), location.href = location.href, location.href = location.pathname, location.replace(location.pathname), location.reload(false)
};

function DismissResponse(idresponse) {
    $.get(`/Home/DismissResponse?idresponse=${idresponse}`);
    location.reload(), history.go(0), location.href = location.href, location.href = location.pathname, location.replace(location.pathname), location.reload(false)
};

function Message() {
    alert('Вы уже откликались на эту вакансию!');
};

function HappyMassage() {
    alert('Отлик записан ʕ•ᴥ•ʔ!');
};

function DeleteMeetMassage() {
    alert('Встреча отменена ʕ◉ᴥ◉ʔ!');
};

function AddMeetMassage() {
    alert('Встреча добавлена ᵔᴥᵔ!');
};

function AddVacancyMassage() {
    alert('Вакансия добавлена ʕ￫ᴥ￩ʔ!');
};

function RedirectResponseToMeeting(id) {
    window.location.href = '/Home/AddMeeting/' + id;
};

function RedirectMeetingToResponse(id) {
    window.location.href = '/Home/Responses/' + id;
};

function RedirectAddVacancyToVacancy(id) {
    window.location.href = '/Home/Vacancy/' + id;

};

function RedirectOneOfTheVacancyToVacancy(id) {
    window.location.href = '/Home/Vacancy/' + id;
};

function RedirectToIndex(id) {
    window.location.href = '/Home/Index/' + id;
};

function PrintResume(id) {
    $.get(`/Print/PrintPesumePDF?iduser=${id}`);
}

function AutoRefresh(t) {
    setTimeout("location.reload(true);", t);
}