import React from "react"
import CustomTextField from "../../controls/custom-text-field"
import AddIcon from '@mui/icons-material/Add';
import CustomSelect from "../../controls/custom-select";
import NumericTextField from "../../controls/numeric-text-field";

const RecipeEditStats = () => {
    const inputStyle = { borderRadius: "5px 0 0 5px", fontSize: "0.9em" }

    return (
        <div className="recipe-stat-info">
            <div className="recipe-edit-sub-header">Image</div>
            <div className="recipe-edit-image">
                {/* <img className="recipe-edit-image-preview" src="/static/images/empty-image.png" /> */}
                <img className="recipe-edit-image-background" src="/static/images/empty-image.png" />
            </div>
            <div className="button-std recipe-edit-image-select">Choose File</div>

            <div className="recipe-edit-sub-header">Difficulty level</div>

            <CustomSelect name={"difficulty"} placeholder={"Difficulty level"} fullWidth />

            <div className="recipe-edit-sub-header">Time</div>
            <NumericTextField name={"time"} placeholder={"Time"} fullWidth />

            <div className="recipe-edit-sub-header">Energy</div>
            <NumericTextField name={"energy"} placeholder={"Energy"} fullWidth />

            <div className="recipe-edit-sub-header">Tags</div>

            <div className="recipe-edit-tag-add">
                <div className="recipe-edit-tag-textbox">
                    <CustomTextField
                        name="tag-add"
                        placeholder="Tag"
                        inputStyles={inputStyle}
                        fullWidth={true} />
                </div>
                <div className="button-std add-button recipe-edit-tag-button">
                    <AddIcon />
                </div>
            </div>

            <div className="recipe-header recipe-edit-tag-header">Tags</div>

            <ul className="recipe-ingridients-list">
                <li className="recipe-ingridient">Tag 1</li>
                <li className="recipe-ingridient">Tag 2</li>
                <li className="recipe-ingridient">Tag 3</li>
            </ul>

        </div>
    )
}

export default RecipeEditStats