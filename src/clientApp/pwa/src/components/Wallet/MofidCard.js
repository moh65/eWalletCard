import card from '../../content/image/card.png'
const MofidCard = () => {

    return (
        <div>
            <br />
            <img src={card}  />

            <br />

            <div className="row mt-5">
                <div className="col-6">
                    <button className="btn btn-primary" style={{fontSize:10}}> افزایش موجودی کارت </button>
                </div>
                <div className="col-6">
                    <button className="btn btn-primary" style={{fontSize:10}}> برداشت موجودی کارت </button>
                </div>
            </div>
        </div>
    )
}

export  default  MofidCard;