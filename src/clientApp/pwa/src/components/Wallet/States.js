
const States = (props) => {
    const { states } = props;
    return (
        <div>

            <div id="timeline-content">
                <ul className="timeline">
                    <li className="event" data-date="65Million B.C.">
                        <strong>  ثبت نام </strong>
                        <p> برای نهایی شدن ثبت نام باید کد و دریافت و ثبت کنید </p>
                    </li>

                    <li className="event" data-date="65Million B.C.">
                        <strong>  نحوه ارسال </strong>
                        <p> از طریق پاد ارسال میشود </p>
                    </li>

                    <li className="event" data-date="65Million B.C.">
                        <strong>  تحویل به پست </strong>
                        <p> کارت شما پس از ایجاد شدن به پست تحویل داده میشود </p>
                    </li>


                    <li className="event" data-date="65Million B.C.">
                        <strong> فعال سازی </strong>
                        <p> بعد از دریافت کارت حتما فعالسازی کنید </p>
                    </li>

                </ul>
            </div>


        </div>
    )
}

export  default  States;