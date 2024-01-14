import React, { useEffect, useState } from "react"
import CustomTextField from "../../controls/custom-text-field"
import CustomSelect from "../../controls/custom-select";
import NumericTextField from "../../controls/numeric-text-field";
import { CreateRecipeTagDto, DifficultyLevel } from "@dietly/sdk";
import RecipeEditTags from "./recipe-edit-tags";
import { toBase64 } from "../../../tools/files";
import AddIcon from '@mui/icons-material/Add';
import { getDifficultyId, getDifficultyName } from "../../../tools/enums";

interface RecipeEditStatsInterface {
    difficultyLevel: DifficultyLevel,
    imageUrl: string,
    time: number,
    calories: number,
    tags: Array<CreateRecipeTagDto> | null,
    onDifficultyLevelChange: (value: DifficultyLevel) => void,
    onImageChange: (value: File) => void,
    onTimeChange: (value: number) => void,
    onCaloriesChange: (value: number) => void,
    onTagsChange: (value: Array<CreateRecipeTagDto>) => void,
}

const inputStyle = { borderRadius: "5px 0 0 5px", fontSize: "0.9em" }

const DEFAULT_DIFFICULTIES: Array<string> = [
    getDifficultyName(DifficultyLevel._0),
    getDifficultyName(DifficultyLevel._1),
    getDifficultyName(DifficultyLevel._2)
]

const RecipeEditStats = (props: RecipeEditStatsInterface) => {
    const { difficultyLevel, imageUrl, time, calories, tags,
        onDifficultyLevelChange, onImageChange, onTimeChange, onCaloriesChange, onTagsChange } = props;

    const [tag, setTag] = useState<string>("");
    const [imagePreview, setImagePreview] = useState<string>("");
    const [difficultyLevelName, setDifficultyLevelName] = useState<string>("");

    const handleDifficultyChange = (value: string) => {
        setDifficultyLevelName(value)
        onDifficultyLevelChange(getDifficultyId(value))
    }

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

        onImageChange(file)
        try {
            toBase64(file).then((result) => {
                setImagePreview(result as string)
            })
        } catch (error) {
            console.error(error);
            return;
        }
    };

    useEffect(() => {
        setDifficultyLevelName(getDifficultyName(difficultyLevel))
    }, [])

    useEffect(() => {
        setImagePreview(imageUrl)
    }, [imageUrl])

    return (
        <div className="recipe-stat-info">
            <div className="recipe-edit-sub-header">Image</div>
            <div className="recipe-edit-image">
                {imagePreview ?
                    <img className="recipe-edit-image-preview" src={imagePreview} />
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
                values={DEFAULT_DIFFICULTIES}
                value={difficultyLevelName}
                setValue={handleDifficultyChange}
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

