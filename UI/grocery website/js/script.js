//let searchForm = document.querySelector('.search-form');

//document.querySelector('#search-btn').onclick = () =>{
//    searchForm.classList.toggle('active');
//    cart.classList.remove('active');
//    loginForm.classList.remove('active');
//    navbar.classList.remove('active');
//}

//let cart = document.querySelector('.shopping-cart');

//document.querySelector('#cart-btn').onclick = () =>{
//    cart.classList.toggle('active');
//    searchForm.classList.remove('active');
//    loginForm.classList.remove('active');
//    navbar.classList.remove('active');
//}

//let loginForm = document.querySelector('.login-form');

//document.querySelector('#login-btn').onclick = () =>{
//    loginForm.classList.toggle('active');
//    searchForm.classList.remove('active');
//    cart.classList.remove('active');
//    navbar.classList.remove('active');
//}

//let navbar = document.querySelector('.navbar');

//document.querySelector('#menu-btn').onclick = () =>{
//    navbar.classList.toggle('active');
//    searchForm.classList.remove('active');
//    cart.classList.remove('active');
//    loginForm.classList.remove('active');
//}

//window.onscroll = () =>{
//    searchForm.classList.remove('active');
//    cart.classList.remove('active');
//    loginForm.classList.remove('active');
//    navbar.classList.remove('active');
//}

//let slides = document.querySelectorAll('.home .slides-container .slide');
//let index = 0;

//function next(){
//    slides[index].classList.remove('active');
//    index = (index + 1) % slides.length;
//    slides[index].classList.add('active');
//}

//function prev(){
//    slides[index].classList.remove('active');
//    index = (index - 1 + slides.length) % slides.length;
//    slides[index].classList.add('active');
//}

function validateForm() {
    let isValid = true;

    // E-posta kontrolü
    let email = document.getElementById("Email").value.trim();
    let emailError = document.getElementById("emailError");
    let emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zAZ]{2,}$/;
    if (!emailPattern.test(email)) {
        emailError.innerText = "Geçerli bir e-posta adresi girin.";
        emailError.style.display = "block";
        isValid = false;
    } else {
        emailError.style.display = "none";
    }

    // Adý Soyadý kontrolü
    let name = document.getElementById("Name").value.trim();
    let nameError = document.getElementById("nameError");
    if (name.length < 3) {
        nameError.innerText = "Adýnýz en az 3 karakter olmalýdýr.";
        nameError.style.display = "block";
        isValid = false;
    } else {
        nameError.style.display = "none";
    }

    // Telefon kontrolü
    let phone = document.getElementById("Phone").value.trim();
    let phoneError = document.getElementById("phoneError");
    let phonePattern = /^[0-9]{10}$/; // 10 haneli rakam olmalý (5XX1234567)
    if (!phonePattern.test(phone)) {
        phoneError.innerText = "Telefon numarasý 10 haneli olmalýdýr. (5XX1234567)";
        phoneError.style.display = "block";
        isValid = false;
    } else {
        phoneError.style.display = "none";
    }

    // Adres kontrolü
    let address = document.getElementById("Address").value.trim();
    let addressError = document.getElementById("addressError");
    if (address.length < 10) {
        addressError.innerText = "Adres en az 10 karakter olmalýdýr.";
        addressError.style.display = "block";
        isValid = false;
    } else {
        addressError.style.display = "none";
    }

    return isValid;
}