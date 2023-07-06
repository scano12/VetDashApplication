import { SearchBar } from "./SearchBar";
import { UserNav } from "./UserNav";
import "./index.css";

export const Header = () => {
  return (
    <>
      <header className="header">
        <img
          src="../../../../public/paw-logo.png"
          alt="vet-logo"
          className="logo"
        />
        <SearchBar />
        <UserNav />
      </header>
    </>
  );
};
