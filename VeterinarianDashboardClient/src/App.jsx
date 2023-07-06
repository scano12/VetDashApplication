import { useState, useEffect } from "react";
import "./index.css";
import { Header } from "./components/header/header";
import { Sidebar } from "./components/sidebar/sidebar";
import { Main } from "./components/main/Main";
import { GlobalContext } from "./Context";
import { ErrorModal } from "./components/share/ErrorModal";

function App() {
  const [accepted, setAccepted] = useState([]);
  const [items, setItems] = useState([]);
  const [displayErrorModal, setDisplayErrorModal] = useState(false);
  const [displayErrorDuplicationModal, setDisplayErrorDuplicationModal] =
    useState(false);
  const [date, setDate] = useState(new Date());
  const [open, setOpen] = useState(false);

  useEffect(() => {
    async function fetchAppointmentRequestsData() {
      const response = await fetch(
        "https://localhost:7001/api/vetAppointmentApi",
        {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
          },
          mode: "cors",
        }
      );
      const jsonData = await response.json();

      setItems(jsonData.data);
    }

    async function fetchConfirmedAppointmentData() {
      const response = await fetch(
        "https://localhost:7001/api/vetAppointmentApi/ConfirmedAppointments",
        {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
          },
          mode: "cors",
        }
      );
      const jsonData = await response.json();

      setAccepted(jsonData.data);
    }

    fetchAppointmentRequestsData();
    fetchConfirmedAppointmentData();
  }, []);

  return (
    <>
      <div className="container">
        <Header />
        <div
          className="content"
          style={displayErrorModal ? { filter: "blur(2px)" } : {}}
        >
          <GlobalContext.Provider
            value={{
              items,
              setItems,
              accepted,
              setAccepted,
              displayErrorModal,
              setDisplayErrorModal,
              setDisplayErrorDuplicationModal,
              date,
              setDate,
              open,
              setOpen,
            }}
          >
            <Sidebar />
            <Main />
          </GlobalContext.Provider>
        </div>
        {displayErrorModal && (
          <ErrorModal
            onOkClick={() => setDisplayErrorModal(false)}
            headerOneText={"Time Overlap"}
            headerTwoText={"Please Re-Schedule"}
          />
        )}
        {displayErrorDuplicationModal && (
          <ErrorModal
            onOkClick={() => setDisplayErrorDuplicationModal(false)}
            headerOneText={"Cannot accept duplication of appointment"}
          />
        )}
      </div>
    </>
  );
}

export default App;
