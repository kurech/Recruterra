function CreateResponse(iduser, idvacancy, date) {
    $.get(`/Home/AddResponse?idUser=${iduser}&idvacancy=${idvacancy}&dateandtime=${date}`);
    location.reload(), history.go(0), location.href = location.href, location.href = location.pathname, location.replace(location.pathname), location.reload(true)
};

function CreateMeeting(iduser, name, surname, descrip, dateandtime) {
    $.get(`/Home/AddMeet?iduser=${iduser}&name=${name}&surname=${surname}&descrip=${descrip}&dateandtime=${dateandtime}`);
};

function CreateVacancy(vacposition, vacsalary, vaccity, vacworkex, vacdescrip, vacedu, vactypeofemp) {
    $.get(`/Home/AddVac?vacposition=${vacposition}&vacsalary=${vacsalary}&vaccity=${vaccity}&vacworkex=${vacworkex}&vacdescrip=${vacdescrip}&vacedu=${vacedu}&vactypeofemp=${vactypeofemp}`);
};

function DeleteMeeting(iduser, idmeeting) {
    $.get(`/Home/DelMeeting?iduser=${iduser}&idmeet=${idmeeting}`);
};

function UpdateAccount(id) {
    $.get(`/Home/UpadateAccountAfterLogout?idaccount=${id}`);
    location.reload(), history.go(0), location.href = location.href, location.href = location.pathname, location.replace(location.pathname), location.reload(false)
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
    alert('Отлик записан ᵔᴥᵔ!');
};

function AddVacancyMassage() {
    alert('Вакансия добавлена ʕ￫ᴥ￩ʔ!');
};