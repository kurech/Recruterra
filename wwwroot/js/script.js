function menuDropDown() {
    document.getElementById("myDropdown").classList.toggle("show");
}

function menuDropDown2() {
    document.getElementById("myDropdown2").classList.toggle("show");
}

function SelectedChooseRole(select) {
    const option = select.querySelector(`option[value="${select.value}"]`);
    var temp = document.createElement('div');
    var target = document.getElementById('selectrole');
    if (option.value == "Работодатель") {
        var str = '<p class="font-bolder color-green" id="labelcheckemployer">В дальнейшем нужно будет пройти проверку</p>';

        temp.innerHTML = str;
        while (temp.firstChild) {
            target.appendChild(temp.firstChild);
        }
    }
    else if (option.value === "Соискатель") {
        var elem = document.getElementById('labelcheckemployer');
        elem.parentNode.removeChild(elem);
    }
}

window.onclick = function (event) {
    if (!event.target.matches('.dropbtn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}

function filterDropDown() {
    document.getElementById("myDropdownFilter").classList.toggle("show");
}

function CreateResponse(iduser, idvacancy) {
    $.get(`/Home/AddResponse?iduser=${iduser}&idvacancy=${idvacancy}`).then(() => {
        Swal.fire({
            icon: 'success',
            title: 'Успешно!',
            text: 'Ваш отклик записан!',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            location.reload();
        });
    }).catch(() => {
        Swal.fire({
            icon: 'error',
            title: 'Ошибка!',
            text: 'Не получилось записать отклик! Повторите попытку позже',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            location.reload();
        });
    });
};

function CreateFakeResponse() {
    Swal.fire({
        icon: 'error',
        title: 'Ошибка!',
        text: 'Заполните свое резюме!',
        buttonsStyling: true,
        confirmButtonColor: "#8DD7AB"
    }).then(() => {
        location.reload();
    });
};

function RedirectVacancyToOneOfTheVacancy(idvacancy, iduser) {
    window.location.href = '/Vacancys/OneOfTheVacancy/' + idvacancy + '/' + iduser;
};

function CreateMeeting(idemployer, idresume, dateandtime) {
    $.get(`/Meetings/AdditionalMeeting?idemployer=${idemployer}&idresume=${idresume}&dateandtime=${dateandtime}`).then(() => {
        Swal.fire({
            icon: 'success',
            title: 'Успешно!',
            text: 'Встреча была добавлена',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            window.location.href = '/Settings/Responses';
        });
    }).catch(() => {
        Swal.fire({
            icon: 'error',
            title: 'Ошибка!',
            text: 'Не получилось добавить встречу! Повторите попытку позже',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB",
        }).then(() => {
            window.location.href = '/Settings/Responses';
        });
    });
};

function CreateVacancy(vacposition, vacobligations, vacsalary, vacworkex, vacdescrip, vacedu, vactypeofemp, vacidemployer) {
    $.get(`/Vacancys/AdditionalVacancy?vacposition=${vacposition}&vacobligations=${vacobligations}&vacsalary=${vacsalary}&vacworkex=${vacworkex}&vacdescrip=${vacdescrip}&vacedu=${vacedu}&vactypeofemp=${vactypeofemp}&idemployer=${vacidemployer}`).then(() => {
        Swal.fire({
            icon: 'success',
            title: 'Успешно!',
            text: 'Вакансия в обработке! Подождите пожалуйста, пока ее подтвердят',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            window.location.href = '/Home/Vacancy/';
        });
    }).catch(() => {
        Swal.fire({
            icon: 'error',
            title: 'Ошибка!',
            text: 'Не получилось добавить вакансию! Повторите попытку позже',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            window.location.href = '/Home/Vacancy/';
        });
    });
};

function CreateVacancyFake() {
    Swal.fire({
        icon: 'error',
        title: 'Ошибка!',
        text: 'Ваш аккаунт не подтвержден! Ожидайте проверки..',
        buttonsStyling: true,
        confirmButtonColor: "#8DD7AB"
    }).then(() => {
        window.location.href = '/Home/Vacancy/';
    });
}

function DeleteVacancy(idvacancy) {
    $.get(`/Home/DelVacancy?idvacancy=${idvacancy}`).then(() => {
        Swal.fire({
            icon: 'success',
            title: 'Успешно!',
            text: 'Вакансия удалена!',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            location.reload();
        });
    }).catch(() => {
        Swal.fire({
            icon: 'error',
            title: 'Ошибка!',
            text: 'Не получилось удалить вакансию! Повторите попытку позже',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            location.reload();
        });
    });
};

function DeleteVacancyInOOTV(idvacancy) {
    $.get(`/Home/DelVacancy?idvacancy=${idvacancy}`).then(() => {
        Swal.fire({
            icon: 'success',
            title: 'Успешно!',
            text: 'Вакансия удалена!',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            window.location.href = '/Home/Vacancy/';
        });
    }).catch(() => {
        Swal.fire({
            icon: 'error',
            title: 'Ошибка!',
            text: 'Не получилось удалить вакансию! Повторите попытку позже',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            window.location.href = '/Home/Vacancy/';
        });
    });
};

function DeleteMeetingForSeekers(idseeker, idmeeting) {
    $.get(`/Home/DelMeetingSeeker?idseeker=${idseeker}&idmeet=${idmeeting}`).then(() => {
        Swal.fire({
            icon: 'success',
            title: 'Успешно!',
            text: 'Встреча отменена!',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            location.reload();
        });
    }).catch(() => {
        Swal.fire({
            icon: 'error',
            title: 'Ошибка!',
            text: 'Не получилось отменить встречу! Повторите попытку позже',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            location.reload();
        });
    });
};

function DeleteMeetingForEmployers(idemployer, idmeeting) {
    $.get(`/Home/DelMeetingEmployer?idemployer=${idemployer}&idmeet=${idmeeting}`).then(() => {
        Swal.fire({
            icon: 'success',
            title: 'Успешно!',
            text: 'Встреча отменена!',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            location.reload();
        });
    }).catch(() => {
        Swal.fire({
            icon: 'error',
            title: 'Ошибка!',
            text: 'Не получилось отменить встречу! Повторите попытку позже',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            location.reload();
        });
    });
};

function UpdateAccount(id) {
    $.get(`/Access/UpadateAccountAfterLogout?idaccount=${id}`).then(() => {
        window.location.href = '/Home/Index/';
    }).catch(() => {
        Swal.fire({
            icon: 'error',
            title: 'Ошибка!',
            text: 'Не получилось выйти из аккаунта!',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            location.reload();
        });
    });
};

function UpdateProfileResumePage(id, position, salary, edu, university, workex, typeofemp, additionalinformation, itspublic) {
    $.get(`/Settings/UpadateSeekerResumeSettings?iduser=${id}&position=${position}&salary=${salary}&edu=${edu}&university=${university}&workex=${workex}&typeofemp=${typeofemp}&additionalinformation=${additionalinformation}&itspublic=${itspublic}`).then(() => {
        Swal.fire({
            icon: 'success',
            title: 'Успешно!',
            text: 'Ваше резюме успешно обновлено!',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            location.reload();
        });
    }).catch(() => {
        Swal.fire({
            icon: 'error',
            title: 'Ошибка!',
            text: 'Не получилось обновить резюме! Повторите попытку позже',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            location.reload();
        });
    });
}

function UpdateProfileResume(id, rlastname, rfirstname, rmiddlename, rgender, rdateofbirth, rphone, rcity, rcitizenship) {
    $.get(`/Settings/UpadateSeekerProfileSettings?iduser=${id}&lastname=${rlastname}&firstname=${rfirstname}&middlename=${rmiddlename}&gender=${rgender}&dateofbirth=${rdateofbirth}&phone=${rphone}&city=${rcity}&citizenship=${rcitizenship}`).then(() => {
        Swal.fire({
            icon: 'success',
            title: 'Успешно!',
            text: 'Ваши данные успешно обновлены!',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            location.reload();
        });
    }).catch(() => {
        Swal.fire({
            icon: 'error',
            title: 'Ошибка!',
            text: 'Не получилось обновить информацию!',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            location.reload();
        });
    });
}

function UpdateProfileEmployer(id, ecompanyname, emsrn, elastname, efirstname, emiddlename, ecreationdate, ecity, epostcode, estreet, ehouse, eapartment) {
    $.get(`/Settings/UpadateEmployerProfileSettings?iduser=${id}&companyname=${ecompanyname}&msrn=${emsrn}&lastname=${elastname}&firstname=${efirstname}&middlename=${emiddlename}&creationdate=${ecreationdate}&city=${ecity}&postcode=${epostcode}&street=${estreet}&house=${ehouse}&apartment=${eapartment}`).then(() => {
        Swal.fire({
            icon: 'success',
            title: 'Успешно!',
            text: 'Ваши данные успешно обновлены!',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            location.reload();
        });
    }).catch(() => {
        Swal.fire({
            icon: 'error',
            title: 'Ошибка!',
            text: 'Не получилось обновить информацию!',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            location.reload();
        });
    });
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

function AcceptResponse(idresponse, idemployer, idresume) {
    $.get(`/Settings/AcceptResponse?idresponse=${idresponse}`).then(() => {
        Swal.fire({
            icon: 'success',
            title: 'Успешно!',
            text: 'Отклик был принят!',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            AddMettingPage(idemployer, idresume);
        });
    }).catch(() => {
        Swal.fire({
            icon: 'error',
            title: 'Ошибка!',
            text: 'Не получилось принять отклик! Повторите попытку позже',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            location.reload();
        });
    });
};

function DismissResponse(idresponse) {
    $.get(`/Settings/DismissResponse?idresponse=${idresponse}`).then(() => {
        Swal.fire({
            icon: 'success',
            title: 'Успешно!',
            text: 'Отклик был отменен!',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            location.reload();
        });
    }).catch(() => {
        Swal.fire({
            icon: 'error',
            title: 'Ошибка!',
            text: 'Не получилось удалить отклик! Повторите попытку позже',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            location.reload();
        });
    });
};

function RecoverySendCode(email) {
    $.get(`/Access/RecoverySendCodeToEmail?email=${email}`).then(() => {
        var target = document.getElementById('lastelemonpage');
        var str = '<br><div class="field padding-bottom--24 mrtop16"><label for="code">Код подтверждения</label><input type="text" name="code"></div><div class="field"><label for="newpassword">Новый пароль</label><input type="password" name="newpassword"></div><div class="field mrtop16"><input type = "button" name = "submit" value = "Сменить пароль" onclick = "UpdatePassword(this.form.email.value, this.form.code.value, this.form.newpassword.value);"></div>';

        var temp = document.createElement('div');
        temp.innerHTML = str;
        while (temp.firstChild) {
            target.appendChild(temp.firstChild);
        }

        Swal.fire({
            icon: 'success',
            title: 'Успешно!',
            text: `${email} вам отправлен код для восстановления пароля! Проверьте свой почтовый ящик`,
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        });
    }).catch(() => {
        Swal.fire({
            icon: 'error',
            title: 'Ошибка!',
            text: 'Не получилось отправь код восстановления! Повторите попытку позже',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            window.location.href = '/Access/Signin/';
        });
    });
};

function UpdatePassword(email, code, newpassword) {
    $.get(`/Access/UpdatePasswordInRecovery?email=${email}&code=${code}&newpassword=${newpassword}`).then(() => {
        Swal.fire({
            icon: 'success',
            title: 'Успешно!',
            text: 'Ваш пароль был изменен!',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            window.location.href = '/Access/Signin/';
        });
    }).catch(() => {
        Swal.fire({
            icon: 'error',
            title: 'Ошибка!',
            text: 'Произошла ошибка! Повторите попытку позже',
            buttonsStyling: true,
            confirmButtonColor: "#8DD7AB"
        }).then(() => {
            window.location.href = '/Access/Signin/';
        });
    });
}

function AddMettingPage(iduser, idresume) {
    window.location.href = '/Meetings/AddMeeting/' + iduser + '/' + idresume;
};

function RedirectToOneOfTheVacancy(idvacancy, iduser) {
    window.location.href = '/Vacancys/OneOfTheVacancy/' + idvacancy + '/' + iduser;
}

function RedirectResponsesToAddMeeting(iduser, idresume) {
    window.location.href = '/Meetings/AddMeeting/' + iduser + '/' + idresume;
};

function FilterAccept(id, class1, class2, class3, class4, class5) {
    $.get(`/Home/Vacancy?id=${id}&salary=${class1}&city=${class2}&education=${class3}&workexperience=${class4}&employment=${class5}`).then(_ => {
        window.location.href = `/Home/Vacancy?id=${id}&salary=${class1}&city=${class2}&education=${class3}&workexperience=${class4}&employment=${class5}`;
    }); 
}

function RedirectAddMeetingToResponse(id) {
    window.location.href = '/Settings/Responses/' + id;
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