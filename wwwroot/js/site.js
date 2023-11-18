﻿const baseAvatarBtns = document.getElementsByClassName("btn-check");
const avatarAdjSelect = document.getElementById("avatar_adj_select");
const pictureFinishSelect = document.getElementById("avatar_finish_select");
const formSubmitBtn = document.getElementById("submit_form_btn");
const FORM = document.getElementById("form");

let selectedBaseAvatar = null;

Array.from(baseAvatarBtns).forEach((e) => {
  //once user clicks on any base avatar radio input
  //rest of form disabled attributes become false (can select values now)
  e.addEventListener("click", (e) => {
    //if human value, display gender options
    if (e.target.value === "human" && selectedBaseAvatar != "human") {
      selectedBaseAvatar = "human";

      avatarAdjSelect.disabled = true;
      pictureFinishSelect.disabled = true;
      formSubmitBtn.disabled = true;

      const genderOptionDiv = document.createElement("div");
      genderOptionDiv.setAttribute("id", "gender_select_div");
      genderOptionDiv.style.marginBottom = "25px";
      const maleOption = document.createElement("input");
      maleOption.setAttribute("type", "radio");
      maleOption.setAttribute("name", "Gender");
      maleOption.setAttribute("value", "male");
      maleOption.setAttribute("id", "male_option");
      maleOption.style.marginRight = "5px";
      const maleLabel = document.createElement("label");
      maleLabel.setAttribute("for", "male_option");
      maleLabel.innerText = "Male";
      maleLabel.style.marginRight = "20px";

      const femaleOption = document.createElement("input");
      femaleOption.setAttribute("type", "radio");
      femaleOption.setAttribute("name", "Gender");
      femaleOption.setAttribute("value", "female");
      femaleOption.setAttribute("id", "female_option");
      femaleOption.style.marginRight = "5px";
      const femaleLabel = document.createElement("label");
      femaleLabel.setAttribute("for", "female_option");
      femaleLabel.innerText = "Female";

      genderOptionDiv.appendChild(maleOption);
      genderOptionDiv.appendChild(maleLabel);
      genderOptionDiv.appendChild(femaleOption);
      genderOptionDiv.appendChild(femaleLabel);
      document.getElementById("base_select").after(genderOptionDiv);

      maleOption.addEventListener("click", () => {
        avatarAdjSelect.disabled = false;
        pictureFinishSelect.disabled = false;
        formSubmitBtn.disabled = false;
      });
      femaleOption.addEventListener("click", () => {
        avatarAdjSelect.disabled = false;
        pictureFinishSelect.disabled = false;
        formSubmitBtn.disabled = false;
      });
    } else if (e.target.value === "animal" && selectedBaseAvatar != "animal") {
      selectedBaseAvatar = "animal";

      //remove gender select div if present
      if (document.getElementById("gender_select_div") !== null) {
        document.getElementById("gender_select_div").remove();
      }

      avatarAdjSelect.disabled = false;
      pictureFinishSelect.disabled = false;
      formSubmitBtn.disabled = false;
    }
  });
});

//loading spinner when form submit
FORM.addEventListener("submit", () => {
  const loading = document.createElement("div");
  loading.setAttribute("class", "spinner-border text-primary");
  loading.setAttribute("role", "status");

  formSubmitBtn.replaceWith(loading);
});
