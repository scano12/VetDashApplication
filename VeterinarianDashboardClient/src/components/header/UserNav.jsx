import { useEffect, useRef } from "react";
import "./index.css";

export const UserNav = () => {
  const homeBtnRef = useRef(null);

  useEffect(() => {
    homeBtnRef.current.click();
  });

  const navBtnList = document.querySelectorAll("button.user-nav--page");

  navBtnList.forEach((btnEl) => {
    btnEl.addEventListener("click", () => {
      document
        .querySelector("button.user-nav--page.active-state-btn")
        ?.classList.remove("active-state-btn");
      btnEl.classList.add("active-state-btn");
    });
  });

  return (
    <nav className="user-nav">
      <button className="user-nav--page" ref={homeBtnRef}>
        <ion-icon name="paw-outline"></ion-icon>
        Home
      </button>

      <button className="user-nav--page">
        <ion-icon name="chatbubbles-outline"></ion-icon>
        Messages
      </button>

      <button className="user-nav--page">
        <ion-icon name="person"></ion-icon>
        Profile
      </button>
    </nav>
  );
};
