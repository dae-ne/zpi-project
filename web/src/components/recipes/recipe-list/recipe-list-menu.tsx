import React, { useEffect, useState } from "react"
import { DifficultyLevel } from "@dietly/sdk"
import { FormGroup, FormControlLabel, Checkbox, Box, Slider } from "@mui/material"

interface RecipeListMenuInterface {
    tags: Array<string> | null,
    onTagSelectionChange: (value: string[] | undefined) => void,
    onDifficultyLevelChange: (value: any) => void,
    onTimeRangeChange: (min: number, max: number) => void,
    onEnergyRangeChange: (min: number, max: number) => void
}

interface CheckboxData {
    index: number,
    checked: boolean,
    name: string,
    value?: any
}

const checkBoxStyles = { stroke: "#ffffff", strokeWidth: 0.7 }
const MIN_TIME: number = 0;
const MAX_TIME: number = 240;

const MIN_ENERGY: number = 0;
const MAX_ENERGY: number = 1600;

const DEFAULT_DIFF_LEVELS: CheckboxData[] = [{
    index: 0,
    checked: false,
    name: "Easy",
    value: DifficultyLevel._0
}, {
    index: 1,
    checked: false,
    name: "More effort",
    value: DifficultyLevel._1
}, {
    index: 2,
    checked: false,
    name: "Pro",
    value: DifficultyLevel._2
}]

const RecipeListMenu = (props: RecipeListMenuInterface) => {

    const [tagsData, setTagsData] = useState<Array<CheckboxData> | null | undefined>(null)
    const [diffLevelsData, setDiffLevelsData] = useState<Array<CheckboxData>>(DEFAULT_DIFF_LEVELS)
    const [energy, setEnergy] = React.useState<number[]>([MIN_ENERGY, MAX_ENERGY])
    const [time, setTime] = React.useState<number[]>([MIN_TIME, MAX_TIME])

    const { tags, onTagSelectionChange, onDifficultyLevelChange, onTimeRangeChange, onEnergyRangeChange } = props

    const handleTagSelection = (index: number) => {
        if (!tagsData)
            return;
        const tagsCopy = tagsData;
        tagsCopy[index].checked = !tagsCopy[index].checked
        setTagsData([...tagsCopy])
    };

    const handleDiffLevelSelection = (index: number) => {
        const levelsCopy = diffLevelsData;
        levelsCopy[index].checked = !levelsCopy[index].checked
        setDiffLevelsData([...levelsCopy])
    };

    const handleSliderChange = (setStateValue: Function, newValue: number | number[]) => {
        setStateValue(newValue as number[]);
    };

    const handleTimeSet = (_: any, newValue: number | number[]) => {
        const range: number[] = newValue as number[];
        onTimeRangeChange(range[0], range[1])
    };

    const handleEnergySet = (_: any, newValue: number | number[]) => {
        const range: number[] = newValue as number[];
        onEnergyRangeChange(range[0], range[1])
    };

    useEffect(() => {
        onDifficultyLevelChange(diffLevelsData?.filter(data => data && data.checked).map((data) => data.index))
    }, [diffLevelsData])

    useEffect(() => {
        onTagSelectionChange(tagsData?.filter(data => data && data.checked).map((data) => data.name))
    }, [tagsData])

    useEffect(() => {
        setTagsData(tags?.map((tag: string, index: number) => {
            return {
                index: index,
                checked: false,
                name: tag
            } as CheckboxData
        }))
    }, [tags])

    useEffect(() => {
        console.log(DEFAULT_DIFF_LEVELS)
    }, [])

    return (
        <div className="recipe-search-menu">
            <Box sx={{ mb: 1 }} className="recipe-search-menu-header">
                Difficulty level
            </Box>

            <FormGroup sx={{ mb: 3 }}>
                {diffLevelsData?.map((level: CheckboxData, index: number) => {
                    return (
                        <FormControlLabel
                            control={<Checkbox
                                value={level.checked}
                                onClick={() => { handleDiffLevelSelection(index) }}
                                sx={checkBoxStyles}
                            />}
                            label={level.name}

                            key={"tag" + index} />
                    )
                })}
            </FormGroup>

            <Box sx={{ mb: 1 }} className="recipe-search-menu-header">
                Time
            </Box>
            <Slider
                size="small"
                valueLabelDisplay="auto"
                value={time}
                onChange={(_, v) => handleSliderChange(setTime, v)}
                onChangeCommitted={handleTimeSet}
                min={MIN_TIME}
                max={MAX_TIME}
                sx={{ mb: 3 }}
            />

            <Box sx={{ mb: 1 }} className="recipe-search-menu-header">
                Energy
            </Box>
            <Slider
                size="small"
                valueLabelDisplay="auto"
                value={energy}
                onChange={(_, v) => handleSliderChange(setEnergy, v)}
                onChangeCommitted={handleEnergySet}
                min={MIN_ENERGY}
                max={MAX_ENERGY}
                sx={{ mb: 3 }}
            />
            <Box sx={{ mb: 1 }} className="recipe-search-menu-header">
                Tags
            </Box>

            <FormGroup sx={{ mb: 3 }}>
                {tagsData?.map((tag: CheckboxData, index: number) => {
                    return (
                        <FormControlLabel
                            control={<Checkbox
                                value={tag.checked}
                                onClick={() => { handleTagSelection(index) }}
                                sx={checkBoxStyles}
                            />}
                            label={tag.name}

                            key={"tag" + index} />
                    )
                })}
            </FormGroup>
        </div>
    )
}

export default RecipeListMenu
