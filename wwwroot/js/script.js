function CreateResponse(iduser, idvacancy) {
    $.get(`/Home/AddResponse?iduser=${iduser}&idvacancy=${idvacancy}`);
};

function RedirectVacancyToOneOfTheVacancy(idvacancy, iduser) {
    window.location.href = '/Vacancys/OneOfTheVacancy/' + idvacancy + '/' + iduser;
};

function CreateMeeting(idemployer, idresume, dateandtime) {
    $.get(`/Home/AddMeet?idemployer=${idemployer}&idresume=${idresume}&dateandtime=${dateandtime}`);
};

function CreateVacancy(vacposition, vacobligations, vacsalary, vacworkex, vacdescrip, vacedu, vactypeofemp, vacisactive, vacidemployer) {
    $.get(`/Home/AddVac?vacposition=${vacposition}&vacobligations=${vacobligations}&vacsalary=${vacsalary}&vacworkex=${vacworkex}&vacdescrip=${vacdescrip}&vacedu=${vacedu}&vactypeofemp=${vactypeofemp}&vacisactive=${vacisactive}&idemployer=${vacidemployer}`);
};

function DeleteVacancy(idvacancy) {
    $.get(`/Home/DelVacancy?idvacancy=${idvacancy}`);
}

function DeleteMeetingForSeekers(idseeker, idmeeting) {
    $.get(`/Home/DelMeetingSeeker?idseeker=${idseeker}&idmeet=${idmeeting}`);
};

function DeleteMeetingForEmployers(idemployer, idmeeting) {
    $.get(`/Home/DelMeetingEmployer?idemployer=${idemployer}&idmeet=${idmeeting}`);
};

function UpdateAccount(id) {
    $.get(`/Home/UpadateAccountAfterLogout?idaccount=${id}`);
    window.location.href = '/Home/Index/';
};

function UpdateProfileResumePage(id, photo, postcode, street, house, apartment, position, salary, edu, university, workex, typeofemp, additionalinformation, itspublic) {
    console.log(photo);
    $.get(`/Print/UpadateSeekerResumeSettings?iduser=${id}&photo=${photo}&postcode=${postcode}&street=${street}&house=${apartment}&apartment=${apartment}&position=${position}&salary=${salary}&edu=${edu}&university=${university}&workex=${workex}&typeofemp=${typeofemp}&additionalinformation=${additionalinformation}&itspublic=${itspublic}`);
}

function UpdateProfileResume(id, rlogin, rlastname, rfirstname, rmiddlename, rgender, rdateofbirth, rphone, rcity, rcitizenship) {
    $.get(`/Home/UpadateSeekerProfileSettings?iduser=${id}&login=${rlogin}&lastname=${rlastname}&firstname=${rfirstname}&middlename=${rmiddlename}&gender=${rgender}&dateofbirth=${rdateofbirth}&phone=${rphone}&city=${rcity}&citizenship=${rcitizenship}`);
}

function UpdateProfileEmployer(id, elogin, ecompanyname, emsrn, elastname, efirstname, emiddlename, ecreationdate, ecity, epostcode, estreet, ehouse, eapartment) {
    $.get(`/Home/UpadateEmployerProfileSettings?iduser=${id}&login=${elogin}&companyname=${ecompanyname}&msrn=${emsrn}&lastname=${elastname}&firstname=${efirstname}&middlename=${emiddlename}&creationdate=${ecreationdate}&city=${ecity}&postcode=${epostcode}&street=${estreet}&house=${ehouse}&apartment=${eapartment}`);
}

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
};

function DismissResponse(idresponse) {
    $.get(`/Home/DismissResponse?idresponse=${idresponse}`);
};

function RecoverySendCode(email) {
    $.get(`/Home/RecoverySendCodeToEmail?email=${email}`);

    var target = document.getElementById('lastelemonpage');
    var str = '<br><div class="field padding-bottom--24 mrtop16"><label for="code">Код подтверждения</label><input type="text" name="code"></div><div class="field padding-bottom--24 mrtop16"><label for="newpassword">Новый пароль</label><input type="text" name="newpassword"></div><div class="field padding-bottom--24"><input type = "button" name = "submit" value = "Продолжить" onclick = "UpdatePassword(this.form.email.value, this.form.code.value, this.form.newpassword.value); RedirectToSignin();"></div>';

    var temp = document.createElement('div');
    temp.innerHTML = str;
    while (temp.firstChild) {
        target.appendChild(temp.firstChild);
    }
};

function UpdatePassword(email, code, newpassword) {
    $.get(`/Home/UpdatePasswordInRecovery?email=${email}&code=${code}&newpassword=${newpassword}`);
}

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

function RedirectResponsesToAddMeeting(iduser, idresume) {
    window.location.href = '/Home/AddMeeting/' + iduser + '/' + idresume;
};

function RedirectAddMeetingToResponse(id) {
    window.location.href = '/ResponsesToVacancies/Responses/' + id;
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

function RedirectToSignin(id) {
    window.location.href = '/Home/Signin/';
};

function PrintResume(id) {
    $.get(`/Print/PrintPesumePDF?iduser=${id}`);
}

function AutoRefresh(t) {
    setTimeout("location.reload(true);", t);
}

function AutoRefreshResponses() {
    window.location.reload(true);
    window.location.href = window.location.href;
}