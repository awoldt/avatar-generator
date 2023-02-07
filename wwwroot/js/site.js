const form = document.getElementById("avatar_form");
const submitBtn = document.getElementById("submit_btn");
const submitBtnArea = document.getElementById("submit_btn_area");
const humanBaseAvatar = document.getElementById("human_radio_select");
const animalBaseAvatar = document.getElementById("animal_radio_select");
const avatarAdjSelect = document.getElementById("avatar_adj_select");
const avatarFinishSelect = document.getElementById("avatar_finish_select");

let showGenderRadioBtns = false;

function e() {
  if (avatarAdjSelect.disabled) {
    avatarAdjSelect.removeAttribute("disabled");
  }
  if (avatarFinishSelect.disabled) {
    avatarFinishSelect.removeAttribute("disabled");
  }
}

humanBaseAvatar.addEventListener("click", e);
humanBaseAvatar.addEventListener("click", () => {
  if (!showGenderRadioBtns) {
    const male = document.createElement("input");
    const female = document.createElement("input");
    male.setAttribute("class", "form-check-input");
    female.setAttribute("class", "form-check-input");
    male.setAttribute("required", "true");
    female.setAttribute("required", "true");
    male.setAttribute("type", "radio");
    male.setAttribute("value", "guy");
    female.setAttribute("type", "radio");
    female.setAttribute("value", "girl");
    male.setAttribute("id", "male_radio_btn");
    female.setAttribute("id", "female_radio_btn");
    male.setAttribute("name", "gender");
    female.setAttribute("name", "gender");

    const maleLabel = document.createElement("label");
    const femaleLabel = document.createElement("label");
    maleLabel.setAttribute("class", "form-check-label");
    femaleLabel.setAttribute("class", "form-check-label");
    maleLabel.setAttribute("for", "male_radio_btn");
    femaleLabel.setAttribute("for", "female_radio_btn");
    maleLabel.innerHTML = "Male";
    femaleLabel.innerHTML = "Female";

    const fieldsetGenderForm = document.createElement("div");
    fieldsetGenderForm.setAttribute("class", "form-group");
    fieldsetGenderForm.setAttribute("id", "gender_radio_div");
    fieldsetGenderForm.style.marginBottom = "25px";

    const maleGenderFormCheck = document.createElement("div");
    maleGenderFormCheck.setAttribute("class", "form-check");
    maleGenderFormCheck.appendChild(male);
    maleGenderFormCheck.appendChild(maleLabel);
    const femaleGenderFormCheck = document.createElement("div");
    femaleGenderFormCheck.setAttribute("class", "form-check");
    femaleGenderFormCheck.appendChild(female);
    femaleGenderFormCheck.appendChild(femaleLabel);
    fieldsetGenderForm.appendChild(maleGenderFormCheck);
    fieldsetGenderForm.appendChild(femaleGenderFormCheck);

    document.getElementsByTagName("p")[2].before(fieldsetGenderForm);
    showGenderRadioBtns = true;
  }
});
animalBaseAvatar.addEventListener("click", e);
animalBaseAvatar.addEventListener("click", () => {
  if (showGenderRadioBtns) {
    document.getElementById("gender_radio_div").remove();
    showGenderRadioBtns = false;
  }
});
form.onsubmit = () => {
  submitBtn.remove();
  const spinner = document.createElement("div");
  spinner.setAttribute("class", "spinner-border text-info");
  spinner.setAttribute("role", "status");
  submitBtnArea.appendChild(spinner);
};
