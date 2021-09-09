import {Link} from "react-router-dom";

const InActiveCard = () => {
    return (
        <>
            <div className={"mask-bank"}>
              <Link to={"/active"} className={"btn btn-primary"}> فعالسازی </Link>
            </div>
        </>
    )
}

export  default  InActiveCard;