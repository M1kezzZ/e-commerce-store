import { Checkbox, FormControlLabel } from "@mui/material";
import { useController, UseControllerProps } from "react-hook-form";
import React, { useEffect, useState } from 'react';

interface Props extends UseControllerProps {
    label: string;
    disabled: boolean;
}

export default function AppCheckbox(props: Props) {
    const { field } = useController({ ...props, defaultValue: false });
    const [isDisabled, setIsDisabled] = useState(props.disabled);

    useEffect(() => {
        setIsDisabled(props.disabled);
    }, [props.disabled]);

    return (
        <FormControlLabel
            control={
                <Checkbox
                    {...field}
                    checked={field.value}
                    color="secondary"
                    disabled={isDisabled}
                />
            }
            label={props.label}
        />
    );
}
