import React, { useEffect, useState } from "react"
import { Box } from "@mui/material"
import AddIcon from '@mui/icons-material/Add';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import ListIcon from '@mui/icons-material/List';

import "./plans.scss"
import PlansListElement from "./plans-list-element";
import appTheme from "../theme";
import moment from "moment";
import CloseIcon from '@mui/icons-material/Close';
import RecipeList from "../recipes/recipe-list";
import { RecipeListMode } from "../../enums/recipe";
import { GetRecipeResponse } from "../../sdk";

const DAYS: Array<string> = [
    "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"
]

const YESTERDAY: string = "Yesterday: ";
const TODAY: string = "Today: ";
const TOMORROW: string = "Tomorrow: ";

const DateDiffInDays = (date1: Date, date2: Date): number => {
    return moment(date2).startOf('day').diff(moment(date1).startOf('day'), 'days')
}

const PlansList = () => {
    const [dayOfWeek, setDayOfWeek] = useState<number>((new Date).getDay() - 1)
    const [dayPrefix, setDayPrefix] = useState<string>("")
    const [selectedDay, setSelectedDay] = useState<Date>(new Date)

    const [recipePanelVisible, setRecipePanelVisible] = useState<boolean>(true)

    const setDates = (operation: number) => {
        const newDay = moment(selectedDay).add(operation, 'days').toDate();
        const newDayOfWeek = (newDay.getDay() - 1 + DAYS.length) % DAYS.length

        setSelectedDay(newDay)
        setDayOfWeek(newDayOfWeek)
        calcDayPrefix(newDay)
    }

    const calcDayPrefix = (newDay: Date) => {
        const dayFromTodayDiff = DateDiffInDays(new Date, newDay);
        //    console.log(dayFromTodayDiff)
        if (dayFromTodayDiff === -1) {
            setDayPrefix(YESTERDAY)
        } else if (dayFromTodayDiff === 0) {
            setDayPrefix(TODAY)
        } else if (dayFromTodayDiff === 1) {
            setDayPrefix(TOMORROW)
        } else {
            setDayPrefix("")
        }
    }

    const addNewMeal = (date: Date, recipe: GetRecipeResponse) => {
        console.log(date, recipe)
    }

    useEffect(() => {
        calcDayPrefix(new Date)
    }, [])
    return (
        <>
            <Box sx={{ pt: 3, display: 'flex', justifyContent: "space-between" }}>

                <div className="plans-list-navigation">
                    <div className="plans-list-navigation-arrows">
                        <div className="button-std nav-button nav-button-left">
                            <ArrowBackIcon htmlColor="black" fontSize="small" onClick={() => setDates(-1)} />
                        </div>
                        <div className="button-std nav-button nav-button-right">
                            <ArrowForwardIcon htmlColor="black" fontSize="small" onClick={() => setDates(1)} />
                        </div>
                    </div>

                    <div className="plans-list-navigation-day">{dayPrefix}</div>
                    <div className="plans-list-navigation-date">{(moment(selectedDay)).format('dddd, DD MMM')}</div>
                </div>

                <div className="plans-list-buttons">
                    {/* <div className="button-std add-button plans-list-menu-item" >
                        <ListIcon />
                    </div> */}

                    <div className="button-std add-button" onClick={() => setRecipePanelVisible(true)}>
                        <AddIcon />
                    </div>
                </div>

            </Box>

            <Box sx={{ pt: 3 }}>
                <div className="plans-list-week-days">
                    {DAYS.map((day: string, index: number) => {
                        return (
                            <div
                                className="plans-list-day"
                                key={"day" + index}
                                style={{ backgroundColor: index == dayOfWeek ? appTheme.palette.primary.main : "transparent" }}
                            >{day}</div>)
                    })}
                </div>
            </Box>
            <Box sx={{ pt: 3 }}>
                <PlansListElement />
            </Box>

            {recipePanelVisible &&
                <div className="plans-list-add-container">
                    <div className="plans-list-recipe-list-wrapper">
                        <CloseIcon className="plans-list-recipe-list-close" onClick={() => setRecipePanelVisible(false)} />
                        <RecipeList mode={RecipeListMode.Select} onAccept={addNewMeal} />
                    </div>
                </div>
            }

        </>
    )
}

export default PlansList