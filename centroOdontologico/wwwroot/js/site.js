







const classActive = () => {









    let packName = location.pathname.toLowerCase();

    document.querySelectorAll('li.sidebar-item').forEach(e => {

        if (packName.toString() == e.children[0].getAttribute("href").toLowerCase().toString()) {
            e.classList.add("active");
        }
    });

}





classActive();