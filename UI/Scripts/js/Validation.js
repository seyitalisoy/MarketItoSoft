function validateForm() {
    let isValid = true;

    // Email kontrolü
    let email = document.getElementById("Email").value.trim();
    let emailError = document.getElementById("emailError");
    let emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    if (!emailPattern.test(email)) {
        emailError.innerText = "Geçerli bir e-posta adresi girin.";
        emailError.style.display = "block";
        isValid = false;
    } else {
        emailError.style.display = "none";
    }

    // Adı Soyadı kontrolü
    let name = document.getElementById("Name").value.trim();
    let nameError = document.getElementById("nameError");
    if (name.length < 3) {
        nameError.innerText = "Adınız en az 3 karakter olmalıdır.";
        nameError.style.display = "block";
        isValid = false;
    } else {
        nameError.style.display = "none";
    }

    // Telefon kontrolü
    let phone = document.getElementById("Phone").value.trim();
    let phoneError = document.getElementById("phoneError");
    let phonePattern = /^[0-9]{10}$/; // 10 haneli rakam olmalı (5XX1234567)
    if (!phonePattern.test(phone)) {
        phoneError.innerText = "Telefon numarası 10 haneli olmalıdır. (5XX1234567)";
        phoneError.style.display = "block";
        isValid = false;
    } else {
        phoneError.style.display = "none";
    }

    // Adres kontrolü
    let address = document.getElementById("Address").value.trim();
    let addressError = document.getElementById("addressError");
    if (address.length < 10) {
        addressError.innerText = "Adres en az 10 karakter olmalıdır.";
        addressError.style.display = "block";
        isValid = false;
    } else {
        addressError.style.display = "none";
    }

    return isValid;
}
