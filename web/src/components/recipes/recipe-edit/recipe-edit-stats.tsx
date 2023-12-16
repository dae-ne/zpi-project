import React, { useState } from "react"
import CustomTextField from "../../controls/custom-text-field"
import AddIcon from '@mui/icons-material/Add';
import CustomSelect from "../../controls/custom-select";
import NumericTextField from "../../controls/numeric-text-field";
import { CreateRecipeIngredientDto, CreateRecipeTagDto, DifficultyLevel } from "../../../sdk";
import RecipeEditTags from "./recipe-edit-tags";
import Button from "@mui/material/Button";
import DeleteIcon from '@mui/icons-material/Delete';
import { IconButton, Tooltip } from "@mui/material";
import { toBase64 } from "../../../tools/files";


interface RecipeEditStatsInterface {
    difficultyLevel: DifficultyLevel,
    imageUrl: string,
    time: number,
    calories: number,
    tags: Array<CreateRecipeTagDto> | null,
    onDifficultyLevelChange: (value: DifficultyLevel) => void,
    onImageUrlChange: (value: string) => void,
    onTimeChange: (value: number) => void,
    onCaloriesChange: (value: number) => void,
    onTagsChange: (value: Array<CreateRecipeTagDto> | null) => void,
}
const inputStyle = { borderRadius: "5px 0 0 5px", fontSize: "0.9em" }

const RecipeEditStats = (props: RecipeEditStatsInterface) => {
    const [tag, setTag] = useState<string>("");
    const [image, setImage] = useState<string>("");

    const { difficultyLevel, imageUrl, time, calories, tags,
        onDifficultyLevelChange, onImageUrlChange, onTimeChange, onCaloriesChange, onTagsChange } = props;


    const handleAddTag = () => {
        if (!tag) return;

        const tagTmp: Array<CreateRecipeTagDto> = tags ? tags : new Array<CreateRecipeTagDto>();
        tagTmp.push({ name: tag } as CreateRecipeTagDto);
        onTagsChange(tagTmp);
        setTag("")
    }


    const handleCapture = ({ target }: any) => {
        const file: File | null = target.files[0]

        if (!file) return;
        try {
            toBase64(file).then((result) => {
                setImage(result as string)
            })
        } catch (error) {
            console.error(error);
            return;
        }
    };


    return (
        <div className="recipe-stat-info">
            <div className="recipe-edit-sub-header">Image</div>
            <div className="recipe-edit-image">
                {image ?
                    <img className="recipe-edit-image-preview" src={image} />
                    : <img className="recipe-edit-image-background" src="/static/images/empty-image.png" />
                }
            </div>

            <label htmlFor="fileImage" className="button-std recipe-edit-image-select" >Choose File</label>
            <input hidden accept="image/jpeg" id="fileImage" type="file" onChange={handleCapture} />

            <div className="recipe-edit-sub-header">Difficulty level</div>

            <CustomSelect
                name={"difficulty"}
                placeholder={"Difficulty level"}
                fullWidth
                values={[DifficultyLevel._0, DifficultyLevel._1, DifficultyLevel._2]}
                value={difficultyLevel}
                setValue={onDifficultyLevelChange}
            />

            <div className="recipe-edit-sub-header">Time</div>
            <NumericTextField
                name={"time"}
                placeholder={"Time"}
                fullWidth
                value={time}
                onChange={onTimeChange}
            />

            <div className="recipe-edit-sub-header">Energy</div>
            <NumericTextField
                name={"energy"}
                placeholder={"Energy"}
                fullWidth
                value={calories}
                onChange={onCaloriesChange}
            />

            <div className="recipe-edit-sub-header">Tags</div>

            <div className="recipe-edit-tag-add">
                <div className="recipe-edit-tag-textbox">
                    <CustomTextField
                        name="tag-add"
                        placeholder="Tag"
                        inputStyles={inputStyle}
                        fullWidth={true}
                        value={tag}
                        onChange={setTag} />
                </div>
                <div className="button-std add-button recipe-edit-tag-button" onClick={handleAddTag}>
                    <AddIcon />
                </div>
            </div>

            <RecipeEditTags data={tags} onDataChange={onTagsChange} />

        </div>
    )
}

export default RecipeEditStats

